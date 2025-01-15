using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000048 RID: 72
	public interface IMappedClrTypeAction<T>
	{
		// Token: 0x060002E4 RID: 740
		T ForString();

		// Token: 0x060002E5 RID: 741
		T ForChar();

		// Token: 0x060002E6 RID: 742
		T ForInt32();

		// Token: 0x060002E7 RID: 743
		T ForInt16();

		// Token: 0x060002E8 RID: 744
		T ForUInt16();

		// Token: 0x060002E9 RID: 745
		T ForByte();

		// Token: 0x060002EA RID: 746
		T ForSByte();

		// Token: 0x060002EB RID: 747
		T ForDecimal();

		// Token: 0x060002EC RID: 748
		T ForInt64();

		// Token: 0x060002ED RID: 749
		T ForUInt64();

		// Token: 0x060002EE RID: 750
		T ForUInt32();

		// Token: 0x060002EF RID: 751
		T ForDouble();

		// Token: 0x060002F0 RID: 752
		T ForSingle();

		// Token: 0x060002F1 RID: 753
		T ForBoolean();

		// Token: 0x060002F2 RID: 754
		T ForDateTime();

		// Token: 0x060002F3 RID: 755
		T ForDateTimeOffset();

		// Token: 0x060002F4 RID: 756
		T ForTimeSpan();

		// Token: 0x060002F5 RID: 757
		T ForGuid();

		// Token: 0x060002F6 RID: 758
		T ForByteArray();

		// Token: 0x060002F7 RID: 759
		T ForEntityKey();

		// Token: 0x060002F8 RID: 760
		T ForUnknown();
	}
}
