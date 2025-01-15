using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat.Parameters;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat
{
	// Token: 0x02000B62 RID: 2914
	public class CommandHeader
	{
		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06005CE1 RID: 23777 RVA: 0x0017DEA1 File Offset: 0x0017C0A1
		public List<CommandParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06005CE2 RID: 23778 RVA: 0x0017DEA9 File Offset: 0x0017C0A9
		// (set) Token: 0x06005CE3 RID: 23779 RVA: 0x0017DEB1 File Offset: 0x0017C0B1
		public HeaderType HeaderType { get; private set; }

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x06005CE4 RID: 23780 RVA: 0x0017DEBA File Offset: 0x0017C0BA
		// (set) Token: 0x06005CE5 RID: 23781 RVA: 0x0017DEC2 File Offset: 0x0017C0C2
		public Verb Verb { get; private set; }

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x06005CE6 RID: 23782 RVA: 0x0017DECB File Offset: 0x0017C0CB
		// (set) Token: 0x06005CE7 RID: 23783 RVA: 0x0017DED3 File Offset: 0x0017C0D3
		public Control Control { get; private set; }

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x06005CE8 RID: 23784 RVA: 0x0017DEDC File Offset: 0x0017C0DC
		// (set) Token: 0x06005CE9 RID: 23785 RVA: 0x0017DEE4 File Offset: 0x0017C0E4
		public CompletionCode CompletionCode { get; private set; }

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06005CEA RID: 23786 RVA: 0x0017DEED File Offset: 0x0017C0ED
		// (set) Token: 0x06005CEB RID: 23787 RVA: 0x0017DEF5 File Offset: 0x0017C0F5
		public int ReasonCode { get; private set; }

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06005CEC RID: 23788 RVA: 0x0017DEFE File Offset: 0x0017C0FE
		// (set) Token: 0x06005CED RID: 23789 RVA: 0x0017DF06 File Offset: 0x0017C106
		public int SequenceNumber { get; private set; }

		// Token: 0x06005CEE RID: 23790 RVA: 0x0017DF0F File Offset: 0x0017C10F
		public CommandHeader(Verb verb)
		{
			this.Verb = verb;
			this.HeaderType = HeaderType.ExtendedCommand;
			this.Control = Control.Last;
			this.CompletionCode = CompletionCode.Ok;
			this.SequenceNumber = 1;
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x0017DF46 File Offset: 0x0017C146
		internal CommandHeader()
		{
		}

		// Token: 0x06005CF0 RID: 23792 RVA: 0x0017DF5C File Offset: 0x0017C15C
		internal unsafe void GenerateBytes(byte[] buffer, int offset)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = (int)this.HeaderType;
				*(ptr3++) = 36;
				*(ptr3++) = 3;
				*(ptr3++) = (int)this.Verb;
				*(ptr3++) = this.SequenceNumber;
				*(ptr3++) = (int)this.Control;
				*(ptr3++) = (int)this.CompletionCode;
				*(ptr3++) = 0;
				*(ptr3++) = this.parameters.Count;
				byte* ptr4 = (byte*)ptr3;
				int num = offset + (int)((long)(ptr4 - ptr2));
				Buffer.BlockCopy(this.preparedBytes, 0, buffer, num, this.preparedBytes.Length);
			}
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x0017E000 File Offset: 0x0017C200
		internal unsafe void Extract(byte[] buffer, int offset, bool littleEndian, int encodingCcsid, HisEncoding encoding, bool embeddedCcsid)
		{
			int num;
			int num2;
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				this.HeaderType = (HeaderType)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				ptr3 += 2;
				this.Verb = (Verb)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				this.SequenceNumber = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				this.Control = (Control)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				this.CompletionCode = (CompletionCode)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				this.ReasonCode = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				byte* ptr4 = (byte*)ptr3;
				num2 = offset + (int)((long)(ptr4 - ptr2));
			}
			for (int i = 0; i < num; i++)
			{
				CommandParameter parameterFromBuffer = CommandParameter.GetParameterFromBuffer(buffer, ref num2, littleEndian);
				parameterFromBuffer.Extract(buffer, ref num2, littleEndian, encodingCcsid, encoding, embeddedCcsid);
				this.parameters.Add(parameterFromBuffer);
			}
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x0017E0CC File Offset: 0x0017C2CC
		internal CommandHeader GenerateCopy()
		{
			CommandHeader commandHeader = new CommandHeader();
			commandHeader.CompletionCode = this.CompletionCode;
			commandHeader.Control = this.Control;
			commandHeader.HeaderType = this.HeaderType;
			commandHeader.ReasonCode = this.ReasonCode;
			commandHeader.SequenceNumber = this.SequenceNumber;
			commandHeader.Verb = this.Verb;
			foreach (CommandParameter commandParameter in this.Parameters)
			{
				commandHeader.Parameters.Add(commandParameter.GenerateCopy());
			}
			return commandHeader;
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x0017E178 File Offset: 0x0017C378
		internal byte[] ConvertStructureToBytes(int ccsid, bool embedCcsid)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			int num = 0;
			foreach (CommandParameter commandParameter in this.Parameters)
			{
				num += commandParameter.ConvertStructureToBytes(encoding, ccsid, embedCcsid);
			}
			this.preparedBytes = new byte[num];
			int num2 = 0;
			foreach (CommandParameter commandParameter2 in this.Parameters)
			{
				commandParameter2.ConvertStructureToBytes(this.preparedBytes, ref num2);
			}
			return this.preparedBytes;
		}

		// Token: 0x0400491E RID: 18718
		private byte[] preparedBytes;

		// Token: 0x0400491F RID: 18719
		private List<CommandParameter> parameters = new List<CommandParameter>();
	}
}
