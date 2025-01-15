using System;
using System.Text;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B27 RID: 2855
	public class IdentityContext
	{
		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06005991 RID: 22929 RVA: 0x001718E5 File Offset: 0x0016FAE5
		// (set) Token: 0x06005992 RID: 22930 RVA: 0x001718ED File Offset: 0x0016FAED
		public string UserId
		{
			get
			{
				return this.userId;
			}
			set
			{
				this.userId = Globals.CheckMaximumLength(value, "UserId", 12);
			}
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06005993 RID: 22931 RVA: 0x00171902 File Offset: 0x0016FB02
		// (set) Token: 0x06005994 RID: 22932 RVA: 0x0017190A File Offset: 0x0016FB0A
		public byte[] AccountToken
		{
			get
			{
				return this.accountToken;
			}
			set
			{
				this.accountToken = Globals.CheckExactLength(value, "v", 32);
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06005995 RID: 22933 RVA: 0x0017191F File Offset: 0x0016FB1F
		// (set) Token: 0x06005996 RID: 22934 RVA: 0x00171927 File Offset: 0x0016FB27
		public string ApplicationData
		{
			get
			{
				return this.applicationData;
			}
			set
			{
				this.applicationData = Globals.CheckMaximumLength(value, "ApplicationData", 32);
			}
		}

		// Token: 0x06005997 RID: 22935 RVA: 0x0017193C File Offset: 0x0016FB3C
		internal static IdentityContext GenerateCopy(IdentityContext other, bool create)
		{
			if (other == null || !create)
			{
				return other;
			}
			return new IdentityContext
			{
				AccountToken = ConversionHelpers.ByteArrayNullOrCopy(other.AccountToken, create),
				ApplicationData = other.ApplicationData,
				UserId = other.UserId
			};
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x00171978 File Offset: 0x0016FB78
		internal static int GenerateMqmdBytes(IdentityContext context, byte[] buffer, int offset)
		{
			if (context == null || context.UserId == null)
			{
				buffer[offset] = 0;
			}
			else
			{
				ConversionHelpers.MoveStringToBufferAscii(buffer, offset, context.UserId, 12, false);
			}
			offset += 12;
			if (context == null || context.AccountToken == null)
			{
				for (int i = 0; i < 32; i++)
				{
					buffer[offset + i] = 0;
				}
			}
			else
			{
				for (int j = 0; j < 32; j++)
				{
					buffer[offset + j] = context.AccountToken[j];
				}
			}
			offset += 32;
			if (context == null || context.ApplicationData == null)
			{
				buffer[offset] = 0;
			}
			else
			{
				ConversionHelpers.MoveStringToBufferAscii(buffer, offset, context.ApplicationData, 32, false);
			}
			return 76;
		}

		// Token: 0x06005999 RID: 22937 RVA: 0x00171A10 File Offset: 0x0016FC10
		internal static IdentityContext ExtractMqmd(byte[] buffer, int offset, bool littleEndian, Encoding encoding, out int bytesConsumed)
		{
			bytesConsumed = 76;
			string stringOrNull = ConversionHelpers.GetStringOrNull(buffer, offset, 12, encoding);
			offset += 12;
			byte[] array = new byte[32];
			bool flag = false;
			for (int i = 0; i < 32; i++)
			{
				array[i] = buffer[offset + i];
				if (array[i] != 0)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				array = null;
			}
			offset += 32;
			string stringOrNull2 = ConversionHelpers.GetStringOrNull(buffer, offset, 32, encoding);
			if (stringOrNull == null && array == null && stringOrNull2 == null)
			{
				return null;
			}
			return new IdentityContext
			{
				UserId = stringOrNull,
				AccountToken = array,
				ApplicationData = stringOrNull2
			};
		}

		// Token: 0x04004709 RID: 18185
		private string userId;

		// Token: 0x0400470A RID: 18186
		private byte[] accountToken;

		// Token: 0x0400470B RID: 18187
		private string applicationData;
	}
}
