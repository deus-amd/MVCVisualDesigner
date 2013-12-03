<?xml version="1.0" encoding="utf-8"?>
<Dsl xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="8f2ca638-e08f-4b6f-96a1-ea046a25c190" Description="" Name="MVCVisualDesigner" DisplayName="MVC Visual Designer" Namespace="MVCVisualDesigner" MajorVersion="0" Revision="1" ProductName="MVC Visual Designer" CompanyName="Jun Wang" PackageGuid="2318dda0-8eed-4398-b67d-2e85e627224d" PackageNamespace="MVCVisualDesigner" xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel">
  <Classes>
    <DomainClass Id="404ac32b-b3af-4662-bd2e-14f13a17562b" Description="Description for MVCVisualDesigner.VDWidget" Name="VDWidget" DisplayName="VDWidget" InheritanceModifier="Abstract" Namespace="MVCVisualDesigner" GeneratesDoubleDerived="true">
      <Properties>
        <DomainProperty Id="b3bf12d3-bb09-40bd-85d0-a868753c0d3f" Description="Description for MVCVisualDesigner.VDWidget.Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="VDWidget" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>WidgetHasChildren.Children</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="VDWidgetTitle" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>WidgetHasTitle.Title</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="7c91a0ff-d5ec-467a-b0b8-705ad9049330" Description="Description for MVCVisualDesigner.VDView" Name="VDView" DisplayName="VDView" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="db6a631c-6a2e-494c-b56d-a4f048c50743" Description="Description for MVCVisualDesigner.VDSection" Name="VDSection" DisplayName="VDSection" Namespace="MVCVisualDesigner">
      <BaseClass>
        <DomainClassMoniker Name="VDWidget" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="f9e27ebe-1427-48d9-bba0-b1dfa993ee87" Description="Description for MVCVisualDesigner.VDWidgetTitle" Name="VDWidgetTitle" DisplayName="VDWidget Title" Namespace="MVCVisualDesigner" />
  </Classes>
  <Relationships>
    <DomainRelationship Id="8491408d-7160-4a47-9979-e0dab01d52b3" Description="Description for MVCVisualDesigner.WidgetHasChildren" Name="WidgetHasChildren" DisplayName="Widget Has Children" Namespace="MVCVisualDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="223507db-ce6c-4fe1-87aa-33d1adcb6555" Description="Description for MVCVisualDesigner.WidgetHasChildren.SourceVDWidget" Name="SourceVDWidget" DisplayName="Source VDWidget" PropertyName="Children" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Children">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="c0aff184-c06b-475e-b78b-0854c9f15199" Description="Description for MVCVisualDesigner.WidgetHasChildren.TargetVDWidget" Name="TargetVDWidget" DisplayName="Target VDWidget" PropertyName="Parent" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Parent">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="c2d0e6ff-0c0d-4a7d-8ca7-94d2bc2a088c" Description="Description for MVCVisualDesigner.WidgetHasTitle" Name="WidgetHasTitle" DisplayName="Widget Has Title" Namespace="MVCVisualDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="bdb03725-26b0-460b-92ad-c74d0e68d858" Description="Description for MVCVisualDesigner.WidgetHasTitle.VDWidget" Name="VDWidget" DisplayName="VDWidget" PropertyName="Title" Multiplicity="ZeroOne" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Title">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidget" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3e997446-1b1d-4306-b258-b373deba3ada" Description="Description for MVCVisualDesigner.WidgetHasTitle.VDWidgetTitle" Name="VDWidgetTitle" DisplayName="VDWidget Title" PropertyName="Widget" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Widget">
          <RolePlayer>
            <DomainClassMoniker Name="VDWidgetTitle" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
  </Relationships>
  <Types>
    <ExternalType Name="DateTime" Namespace="System" />
    <ExternalType Name="String" Namespace="System" />
    <ExternalType Name="Int16" Namespace="System" />
    <ExternalType Name="Int32" Namespace="System" />
    <ExternalType Name="Int64" Namespace="System" />
    <ExternalType Name="UInt16" Namespace="System" />
    <ExternalType Name="UInt32" Namespace="System" />
    <ExternalType Name="UInt64" Namespace="System" />
    <ExternalType Name="SByte" Namespace="System" />
    <ExternalType Name="Byte" Namespace="System" />
    <ExternalType Name="Double" Namespace="System" />
    <ExternalType Name="Single" Namespace="System" />
    <ExternalType Name="Guid" Namespace="System" />
    <ExternalType Name="Boolean" Namespace="System" />
    <ExternalType Name="Char" Namespace="System" />
    <ExternalType Name="Image" Namespace="System.Drawing" />
  </Types>
  <Shapes>
    <GeometryShape Id="e3f0af00-12a6-4223-861d-180f9ed7f7c3" Description="Description for MVCVisualDesigner.VDWidgetShape" Name="VDWidgetShape" DisplayName="VDWidget Shape" InheritanceModifier="Abstract" Namespace="MVCVisualDesigner" FixedTooltipText="VDWidget Shape" InitialHeight="1" Geometry="Rectangle">
      <Properties>
        <DomainProperty Id="58f89d0b-35cb-4f92-aecd-348184bff87c" Description="" Name="disabled" DisplayName="Disabled" Kind="CustomStorage" Category="Internal States" GetterAccessModifier="FamilyOrAssembly" SetterAccessModifier="FamilyOrAssembly">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d974e6e0-adbc-4b2c-bc4d-cce5d6d87290" Description="" Name="isPinned" DisplayName="Is Pinned" Category="Internal States" GetterAccessModifier="FamilyOrAssembly" SetterAccessModifier="FamilyOrAssembly">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="29d8d234-51c6-433f-881f-b5b9b722ad5a" Description="" Name="titleText" DisplayName="Title Text" Kind="Calculated" Category="Internal States" GetterAccessModifier="FamilyOrAssembly" SetterAccessModifier="FamilyOrAssembly">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="dbf4b4ff-482e-4779-b81a-4107eecf2d05" Description="" Name="titleIcon" DisplayName="Title Icon" Kind="Calculated" Category="Internal States" GetterAccessModifier="FamilyOrAssembly" SetterAccessModifier="FamilyOrAssembly">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing/Image" />
          </Type>
        </DomainProperty>
      </Properties>
    </GeometryShape>
    <GeometryShape Id="4842b8a8-4cb9-429d-ad01-a6d400e0676b" Description="Description for MVCVisualDesigner.VDSectionShape" Name="VDSectionShape" DisplayName="VDSection Shape" Namespace="MVCVisualDesigner" FixedTooltipText="VDSection Shape" InitialHeight="1" Geometry="Rectangle">
      <BaseGeometryShape>
        <GeometryShapeMoniker Name="VDWidgetShape" />
      </BaseGeometryShape>
    </GeometryShape>
    <Port Id="407d285a-e203-4917-a395-730505e148c5" Description="Description for MVCVisualDesigner.VDWidgetTitlePort" Name="VDWidgetTitlePort" DisplayName="VDWidget Title Port" Namespace="MVCVisualDesigner" FixedTooltipText="VDWidget Title Port" InitialWidth="0.5" InitialHeight="0.5" Geometry="Rectangle" />
  </Shapes>
  <XmlSerializationBehavior Name="MVCVisualDesignerSerializationBehavior" Namespace="MVCVisualDesigner">
    <ClassData>
      <XmlClassData TypeName="VDDiagram" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDDiagramMoniker" ElementName="vDDiagram" MonikerTypeName="VDDiagramMoniker">
        <DiagramMoniker Name="VDDiagram" />
      </XmlClassData>
      <XmlClassData TypeName="VDWidget" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDWidgetMoniker" ElementName="vDWidget" MonikerTypeName="VDWidgetMoniker">
        <DomainClassMoniker Name="VDWidget" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="children">
            <DomainRelationshipMoniker Name="WidgetHasChildren" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="VDWidget/Name" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="title">
            <DomainRelationshipMoniker Name="WidgetHasTitle" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="VDView" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDViewMoniker" ElementName="vDView" MonikerTypeName="VDViewMoniker">
        <DomainClassMoniker Name="VDView" />
      </XmlClassData>
      <XmlClassData TypeName="VDSection" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionMoniker" ElementName="vDSection" MonikerTypeName="VDSectionMoniker">
        <DomainClassMoniker Name="VDSection" />
      </XmlClassData>
      <XmlClassData TypeName="WidgetHasChildren" MonikerAttributeName="" SerializeId="true" MonikerElementName="widgetHasChildrenMoniker" ElementName="widgetHasChildren" MonikerTypeName="WidgetHasChildrenMoniker">
        <DomainRelationshipMoniker Name="WidgetHasChildren" />
      </XmlClassData>
      <XmlClassData TypeName="VDWidgetShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDWidgetShapeMoniker" ElementName="vDWidgetShape" MonikerTypeName="VDWidgetShapeMoniker">
        <GeometryShapeMoniker Name="VDWidgetShape" />
        <ElementData>
          <XmlPropertyData XmlName="disabled">
            <DomainPropertyMoniker Name="VDWidgetShape/disabled" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isPinned">
            <DomainPropertyMoniker Name="VDWidgetShape/isPinned" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="titleText" Representation="Ignore">
            <DomainPropertyMoniker Name="VDWidgetShape/titleText" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="titleIcon" Representation="Ignore">
            <DomainPropertyMoniker Name="VDWidgetShape/titleIcon" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="VDSectionShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDSectionShapeMoniker" ElementName="vDSectionShape" MonikerTypeName="VDSectionShapeMoniker">
        <GeometryShapeMoniker Name="VDSectionShape" />
      </XmlClassData>
      <XmlClassData TypeName="VDWidgetTitle" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDWidgetTitleMoniker" ElementName="vDWidgetTitle" MonikerTypeName="VDWidgetTitleMoniker">
        <DomainClassMoniker Name="VDWidgetTitle" />
      </XmlClassData>
      <XmlClassData TypeName="WidgetHasTitle" MonikerAttributeName="" SerializeId="true" MonikerElementName="widgetHasTitleMoniker" ElementName="widgetHasTitle" MonikerTypeName="WidgetHasTitleMoniker">
        <DomainRelationshipMoniker Name="WidgetHasTitle" />
      </XmlClassData>
      <XmlClassData TypeName="VDWidgetTitlePort" MonikerAttributeName="" SerializeId="true" MonikerElementName="vDWidgetTitlePortMoniker" ElementName="vDWidgetTitlePort" MonikerTypeName="VDWidgetTitlePortMoniker">
        <PortMoniker Name="VDWidgetTitlePort" />
      </XmlClassData>
    </ClassData>
  </XmlSerializationBehavior>
  <ExplorerBehavior Name="MVCVisualDesignerExplorer" />
  <Diagram Id="1fd11f50-969f-48aa-af5b-31b4277c4671" Description="Description for MVCVisualDesigner.VDDiagram" Name="VDDiagram" DisplayName="Minimal Language Diagram" Namespace="MVCVisualDesigner" GeneratesDoubleDerived="true">
    <Class>
      <DomainClassMoniker Name="VDView" />
    </Class>
    <ShapeMaps>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDSection" />
        <GeometryShapeMoniker Name="VDSectionShape" />
      </ShapeMap>
      <ShapeMap HasCustomParentElement="true">
        <DomainClassMoniker Name="VDWidgetTitle" />
        <ParentElementPath>
          <DomainPath>WidgetHasTitle.Widget/!VDWidget</DomainPath>
        </ParentElementPath>
        <PortMoniker Name="VDWidgetTitlePort" />
      </ShapeMap>
    </ShapeMaps>
  </Diagram>
  <Designer CopyPasteGeneration="CopyPasteOnly" FileExtension="amvd" EditorGuid="061c971a-6960-4ef9-9c30-8a8a6543b9f7">
    <RootClass>
      <DomainClassMoniker Name="VDView" />
    </RootClass>
    <XmlSerializationDefinition CustomPostLoad="false">
      <XmlSerializationBehaviorMoniker Name="MVCVisualDesignerSerializationBehavior" />
    </XmlSerializationDefinition>
    <ToolboxTab TabText="MVC Visual Designer" />
    <Validation UsesMenu="false" UsesOpen="false" UsesSave="false" UsesLoad="false" />
    <DiagramMoniker Name="VDDiagram" />
  </Designer>
  <Explorer ExplorerGuid="a88f64c9-f30b-4d4a-825d-009ceed441ad" Title="MVC Visual Designer Explorer">
    <ExplorerBehaviorMoniker Name="MVCVisualDesigner/MVCVisualDesignerExplorer" />
  </Explorer>
</Dsl>