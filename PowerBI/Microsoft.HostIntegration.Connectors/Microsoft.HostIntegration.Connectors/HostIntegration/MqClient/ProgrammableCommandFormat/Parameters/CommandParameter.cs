using System;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat.Parameters
{
	// Token: 0x02000BB7 RID: 2999
	public abstract class CommandParameter
	{
		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06005D4C RID: 23884 RVA: 0x0017E2F2 File Offset: 0x0017C4F2
		// (set) Token: 0x06005D4D RID: 23885 RVA: 0x0017E2FA File Offset: 0x0017C4FA
		public ParameterType ParameterType { get; private set; }

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x06005D4E RID: 23886 RVA: 0x0017E303 File Offset: 0x0017C503
		// (set) Token: 0x06005D4F RID: 23887 RVA: 0x0017E30B File Offset: 0x0017C50B
		public int Parameter { get; private set; }

		// Token: 0x06005D50 RID: 23888 RVA: 0x0017E314 File Offset: 0x0017C514
		protected CommandParameter(ParameterType parameterType, int parameter)
		{
			this.ParameterType = parameterType;
			this.Parameter = parameter;
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x0017E32A File Offset: 0x0017C52A
		protected CommandParameter(ParameterType parameterType, int parameter, int structureLength)
		{
			this.ParameterType = parameterType;
			this.Parameter = parameter;
			this.receivedStructureLength = structureLength;
		}

		// Token: 0x06005D52 RID: 23890
		internal abstract void Extract(byte[] buffer, ref int offset, bool littleEndian, int encodingCcsid, HisEncoding encoding, bool embeddedCcsid);

		// Token: 0x06005D53 RID: 23891
		internal abstract CommandParameter GenerateCopy();

		// Token: 0x06005D54 RID: 23892
		internal abstract int ConvertStructureToBytes(HisEncoding encoding, int ccsid, bool embedCcsid);

		// Token: 0x06005D55 RID: 23893
		internal abstract void ConvertStructureToBytes(byte[] bytes, ref int index);

		// Token: 0x06005D56 RID: 23894 RVA: 0x0017E348 File Offset: 0x0017C548
		internal unsafe static CommandParameter GetParameterFromBuffer(byte[] buffer, ref int offset, bool littleEndian)
		{
			CommandParameter commandParameter;
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				ParameterType parameterType = (ParameterType)ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				int num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				int num2 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				offset += (int)((long)((byte*)ptr3 - (byte*)ptr2));
				switch (parameterType)
				{
				case ParameterType.Integer:
					commandParameter = new IntegerParameter(num2);
					goto IL_00DE;
				case ParameterType.String:
					commandParameter = new StringParameter(num2, num);
					goto IL_00DE;
				case ParameterType.IntegerList:
					commandParameter = new IntegerListParameter(num2);
					goto IL_00DE;
				case ParameterType.StringList:
					commandParameter = new StringListParameter(num2, num);
					goto IL_00DE;
				case ParameterType.ByteString:
					commandParameter = new ByteStringParameter(num2, num);
					goto IL_00DE;
				case ParameterType.IntegerFilter:
					commandParameter = new IntegerFilterParameter(num2);
					goto IL_00DE;
				case ParameterType.StringFilter:
					commandParameter = new StringFilterParameter(num2, num);
					goto IL_00DE;
				case ParameterType.ByteStringFilter:
					commandParameter = new ByteStringFilterParameter(num2, num);
					goto IL_00DE;
				}
				throw new InvalidOperationException("Unknown Parameter Type");
				IL_00DE:;
			}
			return commandParameter;
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x0017E438 File Offset: 0x0017C638
		protected int MultipleOf4(int value)
		{
			int num = value % 4;
			if (num == 0)
			{
				return value;
			}
			return value + 4 - num;
		}

		// Token: 0x04004E9B RID: 20123
		protected int receivedStructureLength;
	}
}
