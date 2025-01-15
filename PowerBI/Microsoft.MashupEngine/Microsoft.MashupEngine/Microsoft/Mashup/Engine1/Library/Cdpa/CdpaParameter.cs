using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DA0 RID: 3488
	internal class CdpaParameter
	{
		// Token: 0x06005EFC RID: 24316 RVA: 0x00147DA4 File Offset: 0x00145FA4
		public CdpaParameter(CdpaCube cube, string name, string caption, TypeValue type, bool isRequired, List<Value> allowedValues, Value defaultValue)
		{
			this.cube = cube;
			this.name = name;
			this.caption = caption;
			this.type = type;
			this.isRequired = isRequired;
			this.allowedValues = allowedValues;
			this.defaultValue = defaultValue;
			this.qualifiedName = QualifiedName.New("parameters").Qualify(this.name);
		}

		// Token: 0x17001C0F RID: 7183
		// (get) Token: 0x06005EFD RID: 24317 RVA: 0x00147E07 File Offset: 0x00146007
		public CdpaCube Cube
		{
			get
			{
				return this.cube;
			}
		}

		// Token: 0x17001C10 RID: 7184
		// (get) Token: 0x06005EFE RID: 24318 RVA: 0x00147E0F File Offset: 0x0014600F
		public QualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x17001C11 RID: 7185
		// (get) Token: 0x06005EFF RID: 24319 RVA: 0x00147E17 File Offset: 0x00146017
		public string ParameterName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001C12 RID: 7186
		// (get) Token: 0x06005F00 RID: 24320 RVA: 0x00147E1F File Offset: 0x0014601F
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x17001C13 RID: 7187
		// (get) Token: 0x06005F01 RID: 24321 RVA: 0x00147E27 File Offset: 0x00146027
		public bool IsRequired
		{
			get
			{
				return this.isRequired;
			}
		}

		// Token: 0x17001C14 RID: 7188
		// (get) Token: 0x06005F02 RID: 24322 RVA: 0x00147E2F File Offset: 0x0014602F
		public TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001C15 RID: 7189
		// (get) Token: 0x06005F03 RID: 24323 RVA: 0x00147E37 File Offset: 0x00146037
		public IList<Value> AllowedValues
		{
			get
			{
				return this.allowedValues;
			}
		}

		// Token: 0x17001C16 RID: 7190
		// (get) Token: 0x06005F04 RID: 24324 RVA: 0x00147E3F File Offset: 0x0014603F
		public Value DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x04003420 RID: 13344
		private readonly CdpaCube cube;

		// Token: 0x04003421 RID: 13345
		private readonly string name;

		// Token: 0x04003422 RID: 13346
		private readonly string caption;

		// Token: 0x04003423 RID: 13347
		private readonly QualifiedName qualifiedName;

		// Token: 0x04003424 RID: 13348
		private readonly TypeValue type;

		// Token: 0x04003425 RID: 13349
		private readonly bool isRequired;

		// Token: 0x04003426 RID: 13350
		private readonly List<Value> allowedValues;

		// Token: 0x04003427 RID: 13351
		private readonly Value defaultValue;
	}
}
