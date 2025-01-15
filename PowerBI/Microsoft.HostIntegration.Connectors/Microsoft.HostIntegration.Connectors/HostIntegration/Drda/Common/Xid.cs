using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000849 RID: 2121
	public class Xid
	{
		// Token: 0x0600434C RID: 17228 RVA: 0x000E1BF6 File Offset: 0x000DFDF6
		public Xid()
		{
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x000E1C05 File Offset: 0x000DFE05
		public Xid(int formatid, byte[] globalid, byte[] branchid)
		{
			this._format_id = formatid;
			this._global_id = globalid;
			this._branch_id = branchid;
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x000E1C29 File Offset: 0x000DFE29
		public override string ToString()
		{
			return string.Format("Xid[formatid={0};globalid={1};branchid={2}]", this._format_id, BitUtils.ConvertToHexString(this._global_id), BitUtils.ConvertToHexString(this._branch_id));
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x0600434F RID: 17231 RVA: 0x000E1C56 File Offset: 0x000DFE56
		public int FormatId
		{
			get
			{
				return this._format_id;
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06004350 RID: 17232 RVA: 0x000E1C5E File Offset: 0x000DFE5E
		public byte[] GlobalTransactionId
		{
			get
			{
				return this._global_id;
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06004351 RID: 17233 RVA: 0x000E1C66 File Offset: 0x000DFE66
		public byte[] BranchQualifier
		{
			get
			{
				return this._branch_id;
			}
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x000E1C70 File Offset: 0x000DFE70
		public void Read(DdmReader reader)
		{
			this.ReadAsync(reader, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x000E1C98 File Offset: 0x000DFE98
		public async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			int ddmLength = (int)reader.DdmObjectLength;
			int num = await reader.ReadNetworkIntAsync(isAsync, cancellationToken);
			this._format_id = num;
			int global_id_length = 0;
			int branch_id_length = 0;
			if (this._format_id != -1)
			{
				num = await reader.ReadNetworkIntAsync(isAsync, cancellationToken);
				global_id_length = num;
				num = await reader.ReadNetworkIntAsync(isAsync, cancellationToken);
				branch_id_length = num;
				this._global_id = await reader.ReadBytesAsync(global_id_length, isAsync, cancellationToken);
				this._branch_id = await reader.ReadBytesAsync(branch_id_length, isAsync, cancellationToken);
			}
			int num2 = ddmLength - 12 - global_id_length - branch_id_length;
			if (num2 > 0)
			{
				await reader.SkipBytesAsync(num2, isAsync, cancellationToken);
			}
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x000E1CF8 File Offset: 0x000DFEF8
		public override bool Equals(object obj)
		{
			if (obj is Xid)
			{
				Xid xid = (Xid)obj;
				return this._format_id == xid._format_id && this.ConvertToHexString(this._global_id).Equals(this.ConvertToHexString(xid._global_id), StringComparison.CurrentCultureIgnoreCase) && this.ConvertToHexString(this._branch_id).Equals(this.ConvertToHexString(xid._branch_id), StringComparison.CurrentCultureIgnoreCase);
			}
			return false;
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x000E1D6C File Offset: 0x000DFF6C
		public override int GetHashCode()
		{
			int num = 0;
			if (this._global_id != null)
			{
				num ^= this.ConvertToHexString(this._global_id).GetHashCode();
			}
			if (this._branch_id != null)
			{
				num ^= this.ConvertToHexString(this._branch_id).GetHashCode();
			}
			return num ^ this._format_id;
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x000E1DC0 File Offset: 0x000DFFC0
		public UOWid ToUOWid()
		{
			if (this._format_id != 446 || this._global_id == null || this._global_id.Length != 17 || this._branch_id == null || this._branch_id.Length != 8)
			{
				return null;
			}
			UOWid uowid = new UOWid();
			uowid.NetName = this._global_id;
			uowid.Instance_id = new byte[6];
			Buffer.BlockCopy(this._branch_id, 0, uowid.Instance_id, 0, 6);
			uowid.Sequence_id = (short)(((int)this._branch_id[6] << 8) | (int)this._branch_id[7]);
			return uowid;
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x000E1E50 File Offset: 0x000E0050
		private string ConvertToHexString(byte[] array)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002F79 RID: 12153
		private int _format_id = -1;

		// Token: 0x04002F7A RID: 12154
		private byte[] _branch_id;

		// Token: 0x04002F7B RID: 12155
		private byte[] _global_id;

		// Token: 0x04002F7C RID: 12156
		internal const int DdmXidFormatId = 446;
	}
}
