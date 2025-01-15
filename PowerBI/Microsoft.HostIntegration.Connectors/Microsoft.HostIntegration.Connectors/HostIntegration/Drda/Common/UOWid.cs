using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000847 RID: 2119
	public class UOWid : IComparable<UOWid>
	{
		// Token: 0x0600433A RID: 17210 RVA: 0x000E16F2 File Offset: 0x000DF8F2
		public override string ToString()
		{
			return string.Format("UOWid[netnam={0};instanceid={1};sequenceid={2}]", this._netName, BitUtils.ConvertToHexString(this._instance_id), this._sequence_id);
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x000E171C File Offset: 0x000DF91C
		public UOWid Clone()
		{
			UOWid uowid = new UOWid();
			uowid._netName = this._netName;
			uowid._sequence_id = this._sequence_id;
			uowid._instance_id = new byte[this._instance_id.Length];
			Array.Copy(this._instance_id, uowid._instance_id, this._instance_id.Length);
			return uowid;
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x0600433C RID: 17212 RVA: 0x000E1774 File Offset: 0x000DF974
		// (set) Token: 0x0600433D RID: 17213 RVA: 0x000E177C File Offset: 0x000DF97C
		public byte[] NetName
		{
			get
			{
				return this._netName;
			}
			set
			{
				this._netName = value;
			}
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x0600433E RID: 17214 RVA: 0x000E1785 File Offset: 0x000DF985
		// (set) Token: 0x0600433F RID: 17215 RVA: 0x000E178D File Offset: 0x000DF98D
		public byte[] Instance_id
		{
			get
			{
				return this._instance_id;
			}
			set
			{
				this._instance_id = value;
			}
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06004340 RID: 17216 RVA: 0x000E1796 File Offset: 0x000DF996
		// (set) Token: 0x06004341 RID: 17217 RVA: 0x000E179E File Offset: 0x000DF99E
		public short Sequence_id
		{
			get
			{
				return this._sequence_id;
			}
			set
			{
				this._sequence_id = value;
			}
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x000E17A8 File Offset: 0x000DF9A8
		public void Read(DdmReader reader)
		{
			this.ReadAsync(reader, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x000E17D0 File Offset: 0x000DF9D0
		public async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			byte[] array = await reader.ReadBytesAsync(17, isAsync, cancellationToken);
			this._netName = array;
			array = await reader.ReadBytesAsync(6, isAsync, cancellationToken);
			this._instance_id = array;
			this._sequence_id = await reader.ReadInt16Async(isAsync, cancellationToken);
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x000E182D File Offset: 0x000DFA2D
		public void Write(DdmWriter writer)
		{
			writer.WriteBytes(this._netName);
			writer.WriteBytes(this._instance_id);
			writer.WriteInt16((int)this._sequence_id);
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x000E1854 File Offset: 0x000DFA54
		public override bool Equals(object obj)
		{
			if (obj is UOWid)
			{
				UOWid uowid = (UOWid)obj;
				return this._netName == uowid._netName && this._sequence_id == uowid._sequence_id && this.ConvertToHexString(this._instance_id).Equals(this.ConvertToHexString(uowid._instance_id), StringComparison.CurrentCultureIgnoreCase);
			}
			return false;
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x000E18B4 File Offset: 0x000DFAB4
		public override int GetHashCode()
		{
			int num = 0;
			if (this._instance_id != null)
			{
				num ^= this.ConvertToHexString(this._instance_id).GetHashCode();
			}
			if (this._netName != null)
			{
				num ^= this._netName.GetHashCode();
			}
			return num ^ (int)this._sequence_id;
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x000E1900 File Offset: 0x000DFB00
		public int CompareTo(UOWid other)
		{
			int num = this.ConvertToHexString(this._netName).CompareTo(this.ConvertToHexString(other._netName));
			if (num != 0)
			{
				return num;
			}
			num = this._sequence_id.CompareTo(other._sequence_id);
			if (num != 0)
			{
				return num;
			}
			return this.ConvertToHexString(this._instance_id).CompareTo(this.ConvertToHexString(other._instance_id));
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x000E1964 File Offset: 0x000DFB64
		public Xid ToXid()
		{
			byte[] array = new byte[8];
			byte[] array2 = new byte[17];
			Buffer.BlockCopy(this._netName, 0, array2, 0, 17);
			Buffer.BlockCopy(this._instance_id, 0, array, 0, 6);
			array[6] = (byte)((this._sequence_id >> 8) & 255);
			array[7] = (byte)(this._sequence_id & 255);
			return new Xid(446, array2, array);
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x000E19D0 File Offset: 0x000DFBD0
		private string ConvertToHexString(byte[] array)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002F6E RID: 12142
		private byte[] _netName;

		// Token: 0x04002F6F RID: 12143
		private byte[] _instance_id;

		// Token: 0x04002F70 RID: 12144
		private short _sequence_id;
	}
}
