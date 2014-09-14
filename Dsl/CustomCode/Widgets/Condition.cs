using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public partial class VDCondition
    {
    }
}

namespace MVCVisualDesigner
{
    public partial class VDConditionShape
    {
        DiamondShapeGeometry m_conditionShapeGeometry = null;
        public override ShapeGeometry ShapeGeometry
        {
            get
            {
                if (m_conditionShapeGeometry == null)
                {
                    m_conditionShapeGeometry = new DiamondShapeGeometry();
                }
                return m_conditionShapeGeometry;
            }
        }


        public class DiamondShapeGeometry : NodeShapeGeometry
        {
            public DiamondShapeGeometry()
            {
            }


            /*
            * the function return the 'anchor point' on the shape, lines of links will connect to the 'anchor point'
            * 
            * potentialPoint is on the line which is crossing with the shape, and it's relative to (left, top) of the geometry 
            * bounding box
            * 
            * vectorEndpoint is a vector which defines the slope and direction of the line which is crossing with the shape
            *
            * the crossing point is the returned 'anchor point'
            * 
            * the returned point is also relative to (left, top) of the geometry bounding box
            */
            public override PointD DoFoldToShape(IGeometryHost geometryHost, PointD potentialPoint, PointD vectorEndpoint)
            {
                RectangleD geometryBoundingBox = geometryHost.GeometryBoundingBox;

                PointD A = new PointD(0, geometryBoundingBox.Height / 2);
                PointD B = new PointD(geometryBoundingBox.Width / 2, 0);
                PointD C = new PointD(geometryBoundingBox.Width, geometryBoundingBox.Height / 2);
                PointD D = new PointD(geometryBoundingBox.Width / 2, geometryBoundingBox.Height);

                // corssing point
                PointD[] cp = {
                    getCrossPoint(A, B, potentialPoint, vectorEndpoint),
                    getCrossPoint(B, C, potentialPoint, vectorEndpoint),
                    getCrossPoint(C, D, potentialPoint, vectorEndpoint),
                    getCrossPoint(D, A, potentialPoint, vectorEndpoint)
                };

                double distance = double.MaxValue;
                PointD crossingPoint = PointD.Empty;
                foreach(PointD pt in cp)
                {
                    if (pt.IsEmpty) continue;
                    double dist = getPointDistance(potentialPoint, pt);
                    if (dist < distance)
                    {
                        distance = dist;
                        crossingPoint = pt;
                    }
                }

                return crossingPoint;
            }

            protected override GraphicsPath GetPath(RectangleD boundingBox)
            {
                GraphicsPath diamondShapePath = base.UninitializedPath;
                diamondShapePath.Reset();

                diamondShapePath.StartFigure();

                diamondShapePath.AddLines(new PointF[] { 
                    new PointF((float)(boundingBox.X + boundingBox.Width/2), (float)(boundingBox.Y)),
                    new PointF((float)(boundingBox.X + boundingBox.Width), (float)(boundingBox.Y + boundingBox.Height/2)),
                    new PointF((float)(boundingBox.X + boundingBox.Width/2), (float)(boundingBox.Y + boundingBox.Height)),
                    new PointF((float)(boundingBox.X), (float)(boundingBox.Y + boundingBox.Height/2)),
                });

                diamondShapePath.CloseFigure();

                return diamondShapePath;
            }

            public override bool DoHitTest( IGeometryHost geometryHost, PointD hitPoint, DiagramHitTestInfo hitTestInfo,
                bool includeTolerance)
            {
                RectangleD perimeterBoundingBox = this.GetPerimeterBoundingBox(geometryHost);
                if (includeTolerance)
                {
                    perimeterBoundingBox.Inflate(ShapeGeometry.GetHitTestTolerance(hitTestInfo));
                }
                if (hitTestInfo != null)
                {
                    hitTestInfo.HitDiagramItem = null;
                    hitTestInfo.HitGrabHandle = null;
                }

                // point is in the bounding rectangle
                bool flag = false;
                if (perimeterBoundingBox.Contains(hitPoint)) 
                {
                    // point is in the diamond shape
                    flag = isPointInTriangle(
                            new PointD(perimeterBoundingBox.X, perimeterBoundingBox.Y + perimeterBoundingBox.Height / 2),
                            new PointD(perimeterBoundingBox.X + perimeterBoundingBox.Width / 2, perimeterBoundingBox.Y),
                            new PointD(perimeterBoundingBox.X + perimeterBoundingBox.Width, perimeterBoundingBox.Y + perimeterBoundingBox.Height / 2),
                            hitPoint
                        );
                    if (!flag)
                    {
                        flag = isPointInTriangle(
                                new PointD(perimeterBoundingBox.X, perimeterBoundingBox.Y + perimeterBoundingBox.Height / 2),
                                new PointD(perimeterBoundingBox.X + perimeterBoundingBox.Width / 2, perimeterBoundingBox.Y + perimeterBoundingBox.Height),
                                new PointD(perimeterBoundingBox.X + perimeterBoundingBox.Width, perimeterBoundingBox.Y + perimeterBoundingBox.Height / 2),
                                hitPoint
                            );
                    }
                }

                if (flag && (hitTestInfo != null))
                {
                    hitTestInfo.HitDiagramItem = ShapeGeometry.CreateDiagramItem(geometryHost);
                }
                return flag;
            }

            private static double getPointDistance(PointD p1, PointD p2)
            {
                return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
            }

            // check if line AB crosses with line MN, the corssing point must be within points A and B
            private static PointD getCrossPoint(PointD A, PointD B, PointD M, PointD N)
            {
                // y = k1 * x + c1     (AB)
                // y = k2 * x + c2     (MN)
                double k1 = A.X == B.X ? 1 : (A.Y - B.Y) / (A.X - B.X);
                double c1 = A.X == B.X ? A.X : (A.X * B.Y - B.X * A.Y) / (A.X - B.X);
                double k2 = N.X == 0 ? 1 : N.Y / N.X;// P3.X == P4.X ? 0 : (P3.Y - P4.Y) / (P3.X - P4.X);
                double c2 = M.Y - k2 * M.X;// P3.X == P4.X ? P3.X : (P3.X * P4.Y - P4.X * P3.Y) / (P3.X - P4.X);

                // parallel lines
                if (Math.Abs(k1 - k2) < 0.00001) return PointD.Empty;
                
                // got crossing point
                PointD cp = new PointD((c2 - c1) / (k1 - k2), (k1 * c2 - k2 * c1) / (k1 - k2));

                // check if corssing point is within A & B
                if (isInMid(A.X, B.X, cp.X) && isInMid(A.Y, B.Y, cp.Y))
                    return cp;
                else
                    return PointD.Empty;
            }

            private static bool isInMid(double a, double b, double p)
            {
                if (a < b)
                    return p >= a && p <= b;
                else
                    return p >= b && p <= a;
            }

            private bool isPointInTriangle(PointD a, PointD b, PointD c, PointD p)
            {
                return pointinTriangle(new Vector3D(a.X, a.Y, 0), 
                                        new Vector3D(b.X, b.Y, 0), 
                                        new Vector3D(c.X, c.Y, 0),
                                        new Vector3D(p.X, p.Y, 0));
            }
#region Triangle Algorithm
            class Vector3D
            {
                private double x, y, z;
                public Vector3D(double fx, double fy, double fz)
                {
                    x = fx;
                    y = fy;
                    z = fz;
                }

                // Subtract
                public static Vector3D operator -(Vector3D v1, Vector3D v2)
                {
                    return new Vector3D(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
                }

                // Dot product
                public double Dot(Vector3D v)
                {
                    return x * v.x + y * v.y + z * v.z;
                }

                // Cross product
                public Vector3D Cross(Vector3D v)
                {
                    return new Vector3D(y * v.z - z * v.y, z * v.x - x * v.z, x * v.y - y * v.x);
                }
            };

            // Determine whether two vectors v1 and v2 point to the same direction
            // v1 = Cross(AB, AC)
            // v2 = Cross(AB, AP)
            bool sameSide(Vector3D A, Vector3D B, Vector3D C, Vector3D P)
            {
                Vector3D AB = B - A ;
                Vector3D AC = C - A ;
                Vector3D AP = P - A ;

                Vector3D v1 = AB.Cross(AC) ;
                Vector3D v2 = AB.Cross(AP) ;

                // v1 and v2 should point to the same direction
                return v1.Dot(v2) >= 0 ;
            }

            // Determine whether point P in triangle ABC
            bool pointinTriangle(Vector3D A, Vector3D B, Vector3D C, Vector3D P)
            {
                return sameSide(A, B, C, P) &&
                    sameSide(B, C, A, P) &&
                    sameSide(C, A, B, P) ;
            }
#endregion
        }
    }
}
