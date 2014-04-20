using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner.TypeDescriptor
{

    public class MVDTypeDescriptorBase<T> : MVDGenericTypeDescriptor
    {
        public MVDTypeDescriptorBase(string name, string nameSpace)
            : base(name, nameSpace)
        {
        }

        public override object ParseValue(string raw)
        {
            return ParseValueString(raw);
        }

        public virtual T ParseValueString(string raw)
        {
            return default(T);
        }

        public override bool TryParseValue(string raw, out object value)
        {
            T tValue;
            bool ret = TryParseValueString(raw, out tValue);
            value = tValue;
            return ret;
        }

        public virtual bool TryParseValueString(string raw, out T value)
        {
            value = default(T);
            return false;
        }
    }

    public class MVDIntTypeDescriptor : MVDTypeDescriptorBase<int>
    {
        public MVDIntTypeDescriptor()
            : base("int", null)
        {
        }

        public override int ParseValueString(string raw)
        {
            return int.Parse(raw);
        }

        public override bool TryParseValueString(string raw, out int value)
        {
            return int.TryParse(raw, out value);
        }
    }

    public class MVDUIntTypeDescriptor : MVDTypeDescriptorBase<uint>
    {
        public MVDUIntTypeDescriptor()
            : base("uint", null)
        {
        }

        public override uint ParseValueString(string raw)
        {
            return uint.Parse(raw);
        }

        public override bool TryParseValueString(string raw, out uint value)
        {
            return uint.TryParse(raw, out value);
        }
    }

    public class MVDShortTypeDescriptor : MVDTypeDescriptorBase<short>
    {
        public MVDShortTypeDescriptor()
            : base("short", null)
        {
        }

        public override short ParseValueString(string raw)
        {
            return short.Parse(raw);
        }

        public override bool TryParseValueString(string raw, out short value)
        {
            return short.TryParse(raw, out value);
        }
    }

    public class MVDUShortTypeDescriptor : MVDTypeDescriptorBase<ushort>
    {
        public MVDUShortTypeDescriptor()
            : base("ushort", null)
        {
        }

        public override ushort ParseValueString(string raw)
        {
            return ushort.Parse(raw);
        }

        public override bool TryParseValueString(string raw, out ushort value)
        {
            return ushort.TryParse(raw, out value);
        }
    }

    public class MVDLongTypeDescriptor : MVDTypeDescriptorBase<long>
    {
        public MVDLongTypeDescriptor()
            : base("long", null)
        {
        }

        public override long ParseValueString(string raw)
        {
            return long.Parse(raw);
        }

        public override bool TryParseValueString(string raw, out long value)
        {
            return long.TryParse(raw, out value);
        }
    }

    public class MVDULongTypeDescriptor : MVDTypeDescriptorBase<ulong>
    {
        public MVDULongTypeDescriptor()
            : base("ulong", null)
        {
        }

        public override ulong ParseValueString(string raw)
        {
            return ulong.Parse(raw);
        }

        public override bool TryParseValueString(string raw, out ulong value)
        {
            return ulong.TryParse(raw, out value);
        }
    }

    public class MVDByteTypeDescriptor : MVDTypeDescriptorBase<byte>
    {
        public MVDByteTypeDescriptor()
            : base("byte", null)
        {
        }

        public override byte ParseValueString(string raw)
        {
            return byte.Parse(raw);
        }

        public override bool TryParseValueString(string raw, out byte value)
        {
            return byte.TryParse(raw, out value);
        }
    }

    public class MVDCharTypeDescriptor : MVDTypeDescriptorBase<char>
    {
        public MVDCharTypeDescriptor()
            : base("char", null)
        {
        }

        public override char ParseValueString(string raw)
        {
            return char.Parse(raw);
        }

        public override bool TryParseValueString(string raw, out char value)
        {
            return char.TryParse(raw, out value);
        }
    }

    public class MVDStringTypeDescriptor : MVDTypeDescriptorBase<string>
    {
        public MVDStringTypeDescriptor()
            : base("string", null)
        {
        }

        public override string ParseValueString(string raw)
        {
            return raw;
        }

        public override bool TryParseValueString(string raw, out string value)
        {
            value = raw;
            return true;
        }
    }
}
