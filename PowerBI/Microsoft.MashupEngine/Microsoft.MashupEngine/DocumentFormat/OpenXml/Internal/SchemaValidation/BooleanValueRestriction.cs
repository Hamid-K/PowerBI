using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200312E RID: 12590
	[Serializable]
	internal class BooleanValueRestriction : SimpleTypeRestriction
	{
		// Token: 0x17009957 RID: 39255
		// (get) Token: 0x0601B4F3 RID: 111859 RVA: 0x0037612C File Offset: 0x0037432C
		// (set) Token: 0x0601B4F4 RID: 111860 RVA: 0x00376134 File Offset: 0x00374334
		public override XsdType XsdType
		{
			get
			{
				return this._xsdType;
			}
			set
			{
				this._xsdType = value;
			}
		}

		// Token: 0x17009958 RID: 39256
		// (get) Token: 0x0601B4F5 RID: 111861 RVA: 0x0037613D File Offset: 0x0037433D
		public override string ClrTypeName
		{
			get
			{
				return BooleanValueRestriction.clrTypeName;
			}
		}

		// Token: 0x0400B528 RID: 46376
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = typeof(bool).Name;

		// Token: 0x0400B529 RID: 46377
		private XsdType _xsdType = XsdType.Boolean;
	}
}
