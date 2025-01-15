using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat
{
	// Token: 0x02000B63 RID: 2915
	public class ProgrammableCommandFormatHeader : MqStructuredHeader
	{
		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06005CF4 RID: 23796 RVA: 0x0017E238 File Offset: 0x0017C438
		// (set) Token: 0x06005CF5 RID: 23797 RVA: 0x0017E240 File Offset: 0x0017C440
		public CommandHeader CommandHeader { get; set; }

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06005CF6 RID: 23798 RVA: 0x0017E249 File Offset: 0x0017C449
		public override int AsciiStructId
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06005CF7 RID: 23799 RVA: 0x0017E249 File Offset: 0x0017C449
		public override int EbcdicStructId
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x0017E24C File Offset: 0x0017C44C
		public ProgrammableCommandFormatHeader()
			: base(MqHeaderType.ProgrammableCommandFormat, OrderedMqHeaderType.AllOthers, "PCF Header", "MQADMIN", 36)
		{
		}

		// Token: 0x06005CF9 RID: 23801 RVA: 0x0017E262 File Offset: 0x0017C462
		internal override void GenerateBytes(byte[] buffer, int offset, string format, bool littleEndian, int ccsid, int numericEncodingValue, int ccsidValue)
		{
			this.CommandHeader.GenerateBytes(buffer, offset);
		}

		// Token: 0x06005CFA RID: 23802 RVA: 0x0017E274 File Offset: 0x0017C474
		internal override bool TryExtract(byte[] buffer, int numberOfBytesAvailable, int offset, bool truncationInEffect, ref int ccsidToUse, ref int numericEncodingToUse, out string nextFormat)
		{
			nextFormat = null;
			bool flag = NumericEncoding.EncodingValueIsLittleEndian(numericEncodingToUse);
			HisEncoding hisEncoding = ((ccsidToUse == -1) ? null : HisEncoding.GetEncoding(ccsidToUse));
			this.CommandHeader = new CommandHeader();
			this.CommandHeader.Extract(buffer, offset, flag, ccsidToUse, hisEncoding, hisEncoding == null);
			return true;
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x0017E2C2 File Offset: 0x0017C4C2
		internal override MqHeader GenerateCopy(bool deepCopy)
		{
			if (!deepCopy)
			{
				return this;
			}
			return new ProgrammableCommandFormatHeader
			{
				CommandHeader = this.CommandHeader.GenerateCopy()
			};
		}

		// Token: 0x06005CFC RID: 23804 RVA: 0x0017E2DF File Offset: 0x0017C4DF
		protected override byte[] ConvertStructureToBytes()
		{
			return this.CommandHeader.ConvertStructureToBytes(1252, false);
		}
	}
}
