using System;
using System.IO;
using System.Text;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x020030DC RID: 12508
	internal class SdbDataHead : SdbData
	{
		// Token: 0x17009877 RID: 39031
		// (get) Token: 0x0601B2B0 RID: 111280 RVA: 0x0036F9B0 File Offset: 0x0036DBB0
		public static byte[] SignatureConst
		{
			get
			{
				return Encoding.ASCII.GetBytes("OPENXML SCHM    ");
			}
		}

		// Token: 0x17009878 RID: 39032
		// (get) Token: 0x0601B2B1 RID: 111281 RVA: 0x0036F9CE File Offset: 0x0036DBCE
		// (set) Token: 0x0601B2B2 RID: 111282 RVA: 0x0036F9D6 File Offset: 0x0036DBD6
		public byte[] Signature { get; set; }

		// Token: 0x17009879 RID: 39033
		// (get) Token: 0x0601B2B3 RID: 111283 RVA: 0x0036F9DF File Offset: 0x0036DBDF
		// (set) Token: 0x0601B2B4 RID: 111284 RVA: 0x0036F9E7 File Offset: 0x0036DBE7
		public int DataVersion { get; set; }

		// Token: 0x1700987A RID: 39034
		// (get) Token: 0x0601B2B5 RID: 111285 RVA: 0x0036F9F0 File Offset: 0x0036DBF0
		// (set) Token: 0x0601B2B6 RID: 111286 RVA: 0x0036F9F8 File Offset: 0x0036DBF8
		public int DataByteCount { get; set; }

		// Token: 0x1700987B RID: 39035
		// (get) Token: 0x0601B2B7 RID: 111287 RVA: 0x0036FA01 File Offset: 0x0036DC01
		// (set) Token: 0x0601B2B8 RID: 111288 RVA: 0x0036FA09 File Offset: 0x0036DC09
		public int StartClassId { get; set; }

		// Token: 0x1700987C RID: 39036
		// (get) Token: 0x0601B2B9 RID: 111289 RVA: 0x0036FA12 File Offset: 0x0036DC12
		// (set) Token: 0x0601B2BA RID: 111290 RVA: 0x0036FA1A File Offset: 0x0036DC1A
		public int ClassIdsCount { get; set; }

		// Token: 0x1700987D RID: 39037
		// (get) Token: 0x0601B2BB RID: 111291 RVA: 0x0036FA23 File Offset: 0x0036DC23
		// (set) Token: 0x0601B2BC RID: 111292 RVA: 0x0036FA2B File Offset: 0x0036DC2B
		public int ClassIdsDataOffset { get; set; }

		// Token: 0x1700987E RID: 39038
		// (get) Token: 0x0601B2BD RID: 111293 RVA: 0x0036FA34 File Offset: 0x0036DC34
		// (set) Token: 0x0601B2BE RID: 111294 RVA: 0x0036FA3C File Offset: 0x0036DC3C
		public int SchemaTypeCount { get; set; }

		// Token: 0x1700987F RID: 39039
		// (get) Token: 0x0601B2BF RID: 111295 RVA: 0x0036FA45 File Offset: 0x0036DC45
		// (set) Token: 0x0601B2C0 RID: 111296 RVA: 0x0036FA4D File Offset: 0x0036DC4D
		public int SchemaTypeDataOffset { get; set; }

		// Token: 0x17009880 RID: 39040
		// (get) Token: 0x0601B2C1 RID: 111297 RVA: 0x0036FA56 File Offset: 0x0036DC56
		// (set) Token: 0x0601B2C2 RID: 111298 RVA: 0x0036FA5E File Offset: 0x0036DC5E
		public int ParticleCount { get; set; }

		// Token: 0x17009881 RID: 39041
		// (get) Token: 0x0601B2C3 RID: 111299 RVA: 0x0036FA67 File Offset: 0x0036DC67
		// (set) Token: 0x0601B2C4 RID: 111300 RVA: 0x0036FA6F File Offset: 0x0036DC6F
		public int ParticleDataOffset { get; set; }

		// Token: 0x17009882 RID: 39042
		// (get) Token: 0x0601B2C5 RID: 111301 RVA: 0x0036FA78 File Offset: 0x0036DC78
		// (set) Token: 0x0601B2C6 RID: 111302 RVA: 0x0036FA80 File Offset: 0x0036DC80
		public int ParticleChildrenIndexCount { get; set; }

		// Token: 0x17009883 RID: 39043
		// (get) Token: 0x0601B2C7 RID: 111303 RVA: 0x0036FA89 File Offset: 0x0036DC89
		// (set) Token: 0x0601B2C8 RID: 111304 RVA: 0x0036FA91 File Offset: 0x0036DC91
		public int ParticleChildrenIndexDataOffset { get; set; }

		// Token: 0x17009884 RID: 39044
		// (get) Token: 0x0601B2C9 RID: 111305 RVA: 0x0036FA9A File Offset: 0x0036DC9A
		// (set) Token: 0x0601B2CA RID: 111306 RVA: 0x0036FAA2 File Offset: 0x0036DCA2
		public int AttributeCount { get; set; }

		// Token: 0x17009885 RID: 39045
		// (get) Token: 0x0601B2CB RID: 111307 RVA: 0x0036FAAB File Offset: 0x0036DCAB
		// (set) Token: 0x0601B2CC RID: 111308 RVA: 0x0036FAB3 File Offset: 0x0036DCB3
		public int AttributeDataOffset { get; set; }

		// Token: 0x17009886 RID: 39046
		// (get) Token: 0x0601B2CD RID: 111309 RVA: 0x0036FABC File Offset: 0x0036DCBC
		// (set) Token: 0x0601B2CE RID: 111310 RVA: 0x0036FAC4 File Offset: 0x0036DCC4
		public int SimpleTypeCount { get; set; }

		// Token: 0x17009887 RID: 39047
		// (get) Token: 0x0601B2CF RID: 111311 RVA: 0x0036FACD File Offset: 0x0036DCCD
		// (set) Token: 0x0601B2D0 RID: 111312 RVA: 0x0036FAD5 File Offset: 0x0036DCD5
		public int SimpleTypeDataOffset { get; set; }

		// Token: 0x0601B2D1 RID: 111313 RVA: 0x0036FAE0 File Offset: 0x0036DCE0
		public override byte[] GetBytes()
		{
			byte[] array = new byte[128];
			byte[] bytes = base.GetBytes(new byte[][]
			{
				this.Signature,
				this.DataVersion.Bytes(),
				this.DataByteCount.Bytes(),
				this.StartClassId.Bytes(),
				this.ClassIdsCount.Bytes(),
				this.ClassIdsDataOffset.Bytes(),
				this.SchemaTypeCount.Bytes(),
				this.SchemaTypeDataOffset.Bytes(),
				this.ParticleCount.Bytes(),
				this.ParticleDataOffset.Bytes(),
				this.ParticleChildrenIndexCount.Bytes(),
				this.ParticleChildrenIndexDataOffset.Bytes(),
				this.AttributeCount.Bytes(),
				this.AttributeDataOffset.Bytes(),
				this.SimpleTypeCount.Bytes(),
				this.SimpleTypeDataOffset.Bytes()
			});
			bytes.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0601B2D2 RID: 111314 RVA: 0x0036FBF4 File Offset: 0x0036DDF4
		public override void LoadFromBytes(byte[] value, int startIndex)
		{
			this.Signature = new byte[16];
			Array.Copy(value, startIndex, this.Signature, 0, 16);
			startIndex += 16;
			for (int i = 0; i < 16; i++)
			{
				if (this.Signature[i] != SdbDataHead.SignatureConst[i])
				{
					throw new InvalidDataException("Invalide schema constraint data.");
				}
			}
			this.DataVersion = SdbData.LoadInt(value, ref startIndex);
			this.DataByteCount = SdbData.LoadInt(value, ref startIndex);
			if (this.DataVersion != 65536)
			{
				throw new InvalidDataException("Invalide schema constraint data.");
			}
			this.StartClassId = SdbData.LoadInt(value, ref startIndex);
			this.ClassIdsCount = SdbData.LoadInt(value, ref startIndex);
			this.ClassIdsDataOffset = SdbData.LoadInt(value, ref startIndex);
			this.SchemaTypeCount = SdbData.LoadInt(value, ref startIndex);
			this.SchemaTypeDataOffset = SdbData.LoadInt(value, ref startIndex);
			this.ParticleCount = SdbData.LoadInt(value, ref startIndex);
			this.ParticleDataOffset = SdbData.LoadInt(value, ref startIndex);
			this.ParticleChildrenIndexCount = SdbData.LoadInt(value, ref startIndex);
			this.ParticleChildrenIndexDataOffset = SdbData.LoadInt(value, ref startIndex);
			this.AttributeCount = SdbData.LoadInt(value, ref startIndex);
			this.AttributeDataOffset = SdbData.LoadInt(value, ref startIndex);
			this.SimpleTypeCount = SdbData.LoadInt(value, ref startIndex);
			this.SimpleTypeDataOffset = SdbData.LoadInt(value, ref startIndex);
		}

		// Token: 0x17009888 RID: 39048
		// (get) Token: 0x0601B2D3 RID: 111315 RVA: 0x0036FD37 File Offset: 0x0036DF37
		public override int DataSize
		{
			get
			{
				return 128;
			}
		}

		// Token: 0x0400B3FD RID: 46077
		public const int HeadSize = 128;

		// Token: 0x0400B3FE RID: 46078
		public const int SignatureSize = 16;

		// Token: 0x0400B3FF RID: 46079
		public const int LatestDataVersion = 65536;
	}
}
