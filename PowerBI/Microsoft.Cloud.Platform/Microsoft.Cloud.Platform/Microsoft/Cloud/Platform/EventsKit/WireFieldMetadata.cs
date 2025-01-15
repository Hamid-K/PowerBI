using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200036F RID: 879
	public class WireFieldMetadata : VariableMetadata
	{
		// Token: 0x06001A1A RID: 6682 RVA: 0x00060724 File Offset: 0x0005E924
		public WireFieldMetadata(Type type, AssignedValue value, CustomAttributeData parameterAttribute)
			: this(type, NameUtils.FixNameWithUnderscores(value.ToString()), value, parameterAttribute)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<AssignedValue>(value, "value");
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00060745 File Offset: 0x0005E945
		internal WireFieldMetadata(Type type, string name, AssignedValue value, CustomAttributeData parameterAttribute)
			: base(WireFieldMetadata.KnownType(type), NameUtils.MemberName(name))
		{
			this.Value = value;
			this.ParameterAttribute = parameterAttribute;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00060768 File Offset: 0x0005E968
		// (set) Token: 0x06001A1D RID: 6685 RVA: 0x00060770 File Offset: 0x0005E970
		public AssignedValue Value { get; private set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00060779 File Offset: 0x0005E979
		public bool IsFixedSize
		{
			get
			{
				return !base.VariableMetadataType.Equals(typeof(string)) && !base.VariableMetadataType.Equals(typeof(byte[]));
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x000607AC File Offset: 0x0005E9AC
		public int FieldSize
		{
			get
			{
				if (!this.IsFixedSize)
				{
					return -1;
				}
				if (base.VariableMetadataType.IsEnum)
				{
					return Marshal.SizeOf(typeof(int));
				}
				if (base.VariableMetadataType.Equals(typeof(bool)))
				{
					return Marshal.SizeOf(typeof(short));
				}
				if (base.VariableMetadataType.Equals(typeof(char)))
				{
					return Marshal.SizeOf(typeof(ushort));
				}
				if (base.VariableMetadataType.Equals(typeof(DateTime)))
				{
					return Marshal.SizeOf(typeof(long));
				}
				return Marshal.SizeOf(base.VariableMetadataType);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x00060863 File Offset: 0x0005EA63
		// (set) Token: 0x06001A21 RID: 6689 RVA: 0x0006086B File Offset: 0x0005EA6B
		public CustomAttributeData ParameterAttribute { get; private set; }

		// Token: 0x06001A22 RID: 6690 RVA: 0x00060874 File Offset: 0x0005EA74
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00060881 File Offset: 0x0005EA81
		internal static bool IsKnownType(Type type)
		{
			return WireFieldMetadata.IsPrimitiveType(type) || type.IsEnum;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00060893 File Offset: 0x0005EA93
		private static bool IsPrimitiveType(Type type)
		{
			return WireFieldMetadata.sm_primitiveTypes.Contains(type);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x000608A0 File Offset: 0x0005EAA0
		private static Type KnownType(Type type)
		{
			ExtendedDiagnostics.EnsureOperation(WireFieldMetadata.IsKnownType(type), type.Name + " must be a primitive type or a known enum");
			return type;
		}

		// Token: 0x04000906 RID: 2310
		private static readonly Type[] sm_primitiveTypes = new Type[]
		{
			typeof(short),
			typeof(int),
			typeof(long),
			typeof(ushort),
			typeof(uint),
			typeof(ulong),
			typeof(Guid),
			typeof(bool),
			typeof(string),
			typeof(byte[]),
			typeof(DateTime)
		};
	}
}
