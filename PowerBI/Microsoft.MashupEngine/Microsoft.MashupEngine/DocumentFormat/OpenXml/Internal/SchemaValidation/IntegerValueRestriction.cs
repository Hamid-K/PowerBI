using System;
using System.Diagnostics;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200313B RID: 12603
	[Serializable]
	internal class IntegerValueRestriction : SimpleValueRestriction<long, IntegerValue>
	{
		// Token: 0x17009981 RID: 39297
		// (get) Token: 0x0601B544 RID: 111940 RVA: 0x00376474 File Offset: 0x00374674
		protected override long MinValue
		{
			get
			{
				return long.MinValue;
			}
		}

		// Token: 0x17009982 RID: 39298
		// (get) Token: 0x0601B545 RID: 111941 RVA: 0x0037647F File Offset: 0x0037467F
		protected override long MaxValue
		{
			get
			{
				return long.MaxValue;
			}
		}

		// Token: 0x17009983 RID: 39299
		// (get) Token: 0x0601B547 RID: 111943 RVA: 0x00002461 File Offset: 0x00000661
		// (set) Token: 0x0601B548 RID: 111944 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Integer;
			}
			set
			{
			}
		}

		// Token: 0x17009984 RID: 39300
		// (get) Token: 0x0601B549 RID: 111945 RVA: 0x00376506 File Offset: 0x00374706
		public override string ClrTypeName
		{
			get
			{
				return IntegerValueRestriction.clrTypeName;
			}
		}

		// Token: 0x0400B52F RID: 46383
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = ValidationResources.TypeName_Integer;
	}
}
