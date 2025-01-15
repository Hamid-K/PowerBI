using System;
using System.Diagnostics;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200313C RID: 12604
	[Serializable]
	internal class NonNegativeIntegerValueRestriction : IntegerValueRestriction
	{
		// Token: 0x17009985 RID: 39301
		// (get) Token: 0x0601B54B RID: 111947 RVA: 0x001819C2 File Offset: 0x0017FBC2
		protected override long MinValue
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x0601B54C RID: 111948 RVA: 0x00376519 File Offset: 0x00374719
		public NonNegativeIntegerValueRestriction()
		{
			base.RestrictionField = RestrictionField.MinInclusive;
			base.MinInclusive = 0L;
		}

		// Token: 0x17009986 RID: 39302
		// (get) Token: 0x0601B54D RID: 111949 RVA: 0x00142610 File Offset: 0x00140810
		// (set) Token: 0x0601B54E RID: 111950 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.NonNegativeInteger;
			}
			set
			{
			}
		}

		// Token: 0x17009987 RID: 39303
		// (get) Token: 0x0601B54F RID: 111951 RVA: 0x00376530 File Offset: 0x00374730
		public override string ClrTypeName
		{
			get
			{
				return NonNegativeIntegerValueRestriction.clrTypeName;
			}
		}

		// Token: 0x0400B530 RID: 46384
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = ValidationResources.TypeName_nonNegativeInteger;
	}
}
