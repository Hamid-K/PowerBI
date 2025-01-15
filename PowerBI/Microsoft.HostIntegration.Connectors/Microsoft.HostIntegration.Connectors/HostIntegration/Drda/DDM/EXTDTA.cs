using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000895 RID: 2197
	public class EXTDTA : AbstractDdmObject
	{
		// Token: 0x060045DF RID: 17887 RVA: 0x000F2D12 File Offset: 0x000F0F12
		public EXTDTA(int columnNumber, byte[] blob)
		{
			this._columnNumber = columnNumber;
			this._blob = blob;
			this._isBlob = true;
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x000F2D2F File Offset: 0x000F0F2F
		public EXTDTA(int columnNumber, string clob)
		{
			this._columnNumber = columnNumber;
			this._clob = clob;
			this._isBlob = false;
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x000F2D4C File Offset: 0x000F0F4C
		public EXTDTA()
		{
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x060045E2 RID: 17890 RVA: 0x000F2D54 File Offset: 0x000F0F54
		public int ColumnNumber
		{
			get
			{
				return this._columnNumber;
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x060045E3 RID: 17891 RVA: 0x000F2D5C File Offset: 0x000F0F5C
		public byte[] Blob
		{
			get
			{
				return this._blob;
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x060045E4 RID: 17892 RVA: 0x000F2D64 File Offset: 0x000F0F64
		public string Clob
		{
			get
			{
				return this._clob;
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x060045E5 RID: 17893 RVA: 0x000F2D6C File Offset: 0x000F0F6C
		// (set) Token: 0x060045E6 RID: 17894 RVA: 0x000F2D74 File Offset: 0x000F0F74
		public bool IsBlob
		{
			get
			{
				return this._isBlob;
			}
			set
			{
				this._isBlob = value;
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x060045E7 RID: 17895 RVA: 0x000F2D7D File Offset: 0x000F0F7D
		public object Value
		{
			get
			{
				if (this._isBlob)
				{
					return this._blob;
				}
				return this._clob;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x060045E8 RID: 17896 RVA: 0x000F2D94 File Offset: 0x000F0F94
		// (set) Token: 0x060045E9 RID: 17897 RVA: 0x000F2D9C File Offset: 0x000F0F9C
		public int Length { get; set; }

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x060045EA RID: 17898 RVA: 0x000F2DA5 File Offset: 0x000F0FA5
		// (set) Token: 0x060045EB RID: 17899 RVA: 0x000F2DAD File Offset: 0x000F0FAD
		public ushort Encoding { get; set; }

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x060045EC RID: 17900 RVA: 0x000F2DB6 File Offset: 0x000F0FB6
		// (set) Token: 0x060045ED RID: 17901 RVA: 0x000F2DBE File Offset: 0x000F0FBE
		public bool IsNullable { get; set; }

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x060045EE RID: 17902 RVA: 0x000F2DC7 File Offset: 0x000F0FC7
		// (set) Token: 0x060045EF RID: 17903 RVA: 0x000F2DCF File Offset: 0x000F0FCF
		public bool IsGraphic { get; set; }

		// Token: 0x060045F0 RID: 17904 RVA: 0x000F2DD8 File Offset: 0x000F0FD8
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.EXTDTA);
			writer.WriteByte(0);
			if (this._isBlob)
			{
				if (this._blob != null)
				{
					writer.WriteBytes(this._blob);
				}
				else
				{
					writer.WriteBytes(new byte[1]);
				}
			}
			else if (this._clob != null)
			{
				int num = writer.GenerateStringMBCS(this._clob);
				if (num > 0)
				{
					writer.WriteBytes(num);
				}
			}
			else
			{
				writer.WriteBytes(new byte[1]);
			}
			writer.WriteEndDdm();
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x000F2E58 File Offset: 0x000F1058
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			this._blob = null;
			this._clob = null;
			if (this.Length != 0)
			{
				if (this.IsNullable)
				{
					TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<byte> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<byte>);
					}
					if (taskAwaiter.GetResult() == 255)
					{
						return;
					}
				}
				if (this._isBlob)
				{
					this._blob = await reader.ReadExtDataAsync(this.Length, isAsync, cancellationToken);
				}
				else
				{
					int num = (int)this.Encoding;
					if (num <= 0 && this._database != null && this._database.HostCodePageOverride != -1)
					{
						num = this._database.HostCodePageOverride;
					}
					if (this.IsGraphic)
					{
						if (num <= 0)
						{
							num = ((reader.Ccsid == null || reader.Ccsid._ccsiddbc <= 0) ? 1200 : reader.Ccsid._ccsiddbc);
						}
						this._clob = await reader.ReadExtDSStringAsync(this.Length * 2, num, 54, isAsync, cancellationToken);
					}
					else
					{
						if (num <= 0)
						{
							num = ((reader.Ccsid == null || reader.Ccsid._ccsidsbc <= 0) ? 1208 : reader.Ccsid._ccsidsbc);
						}
						this._clob = await reader.ReadExtStringAsync(this.Length, num, 48, isAsync, cancellationToken);
					}
				}
			}
		}

		// Token: 0x040031C1 RID: 12737
		private int _columnNumber;

		// Token: 0x040031C2 RID: 12738
		private byte[] _blob;

		// Token: 0x040031C3 RID: 12739
		private string _clob;

		// Token: 0x040031C4 RID: 12740
		private bool _isBlob;
	}
}
