using System;
using System.Diagnostics;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200313D RID: 12605
	[Serializable]
	internal class PositiveIntegerValueRestriction : IntegerValueRestriction
	{
		// Token: 0x17009988 RID: 39304
		// (get) Token: 0x0601B551 RID: 111953 RVA: 0x00376543 File Offset: 0x00374743
		protected override long MinValue
		{
			get
			{
				return 1L;
			}
		}

		// Token: 0x0601B552 RID: 111954 RVA: 0x00376547 File Offset: 0x00374747
		public PositiveIntegerValueRestriction()
		{
			base.RestrictionField = RestrictionField.MinInclusive;
			base.MinInclusive = 1L;
		}

		// Token: 0x17009989 RID: 39305
		// (get) Token: 0x0601B553 RID: 111955 RVA: 0x00002475 File Offset: 0x00000675
		// (set) Token: 0x0601B554 RID: 111956 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.PositiveInteger;
			}
			set
			{
			}
		}

		// Token: 0x1700998A RID: 39306
		// (get) Token: 0x0601B555 RID: 111957 RVA: 0x0037655E File Offset: 0x0037475E
		public override string ClrTypeName
		{
			get
			{
				return PositiveIntegerValueRestriction.clrTypeName;
			}
		}

		// Token: 0x0400B531 RID: 46385
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = ValidationResources.TypeName_positiveInteger;
	}
}
