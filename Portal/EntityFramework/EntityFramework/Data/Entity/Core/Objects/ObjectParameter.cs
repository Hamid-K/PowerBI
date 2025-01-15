using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000417 RID: 1047
	public sealed class ObjectParameter
	{
		// Token: 0x0600320B RID: 12811 RVA: 0x000A0CD0 File Offset: 0x0009EED0
		internal static bool ValidateParameterName(string name)
		{
			return DbCommandTree.IsValidParameterName(name);
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x000A0CD8 File Offset: 0x0009EED8
		public ObjectParameter(string name, Type type)
		{
			Check.NotNull<string>(name, "name");
			Check.NotNull<Type>(type, "type");
			if (!ObjectParameter.ValidateParameterName(name))
			{
				throw new ArgumentException(Strings.ObjectParameter_InvalidParameterName(name), "name");
			}
			this._name = name;
			this._type = type;
			this._mappableType = TypeSystem.GetNonNullableType(this._type);
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x000A0D3C File Offset: 0x0009EF3C
		public ObjectParameter(string name, object value)
		{
			Check.NotNull<string>(name, "name");
			Check.NotNull<object>(value, "value");
			if (!ObjectParameter.ValidateParameterName(name))
			{
				throw new ArgumentException(Strings.ObjectParameter_InvalidParameterName(name), "name");
			}
			this._name = name;
			this._type = value.GetType();
			this._value = value;
			this._mappableType = TypeSystem.GetNonNullableType(this._type);
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000A0DAC File Offset: 0x0009EFAC
		private ObjectParameter(ObjectParameter template)
		{
			this._name = template._name;
			this._type = template._type;
			this._mappableType = template._mappableType;
			this._effectiveType = template._effectiveType;
			this._value = template._value;
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x000A0DFB File Offset: 0x0009EFFB
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x000A0E03 File Offset: 0x0009F003
		public Type ParameterType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x000A0E0B File Offset: 0x0009F00B
		// (set) Token: 0x06003212 RID: 12818 RVA: 0x000A0E13 File Offset: 0x0009F013
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06003213 RID: 12819 RVA: 0x000A0E1C File Offset: 0x0009F01C
		// (set) Token: 0x06003214 RID: 12820 RVA: 0x000A0E24 File Offset: 0x0009F024
		internal TypeUsage TypeUsage
		{
			get
			{
				return this._effectiveType;
			}
			set
			{
				this._effectiveType = value;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x000A0E2D File Offset: 0x0009F02D
		internal Type MappableType
		{
			get
			{
				return this._mappableType;
			}
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000A0E35 File Offset: 0x0009F035
		internal ObjectParameter ShallowCopy()
		{
			return new ObjectParameter(this);
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000A0E40 File Offset: 0x0009F040
		internal bool ValidateParameterType(ClrPerspective perspective)
		{
			TypeUsage typeUsage;
			return perspective.TryGetType(this._mappableType, out typeUsage) && TypeSemantics.IsScalarType(typeUsage);
		}

		// Token: 0x0400106B RID: 4203
		private readonly string _name;

		// Token: 0x0400106C RID: 4204
		private readonly Type _type;

		// Token: 0x0400106D RID: 4205
		private readonly Type _mappableType;

		// Token: 0x0400106E RID: 4206
		private TypeUsage _effectiveType;

		// Token: 0x0400106F RID: 4207
		private object _value;
	}
}
