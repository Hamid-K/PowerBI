using System;
using System.Text;
using Microsoft.HostIntegration.StrictResources.Nls;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000621 RID: 1569
	internal class HisCustomEncoding : HisEncoding
	{
		// Token: 0x060034E8 RID: 13544 RVA: 0x000B1104 File Offset: 0x000AF304
		internal HisCustomEncoding(HisEncoding.HostCodePages hostCP, HisCustomEncodingMappings customMaps)
			: base(hostCP)
		{
			if (base.IsMBCSCodePage)
			{
				string text = SR.InvalidCodePageForCustomEncoding((int)hostCP);
				throw new NotSupportedException(text, new DoubleByteCodePagesAreNotSupportedForCustomEncodingException(text));
			}
			try
			{
				this.windowsCodePage = (HisEncoding.WindowsCodePages)Encoding.GetEncoding(customMaps.InheritedCodePage).WindowsCodePage;
				this.windowsEncoding = Encoding.GetEncoding((int)this.windowsCodePage);
			}
			catch (Exception)
			{
			}
			this.hisCustomEncodingMaps = customMaps;
			this.converter = new HisCustomCodePageConverter(this);
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060034E9 RID: 13545 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool IsSingleByte
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x00028FA6 File Offset: 0x000271A6
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x00028FA6 File Offset: 0x000271A6
		public override int GetMaxByteCount(int charCount)
		{
			return charCount;
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060034EC RID: 13548 RVA: 0x000B1188 File Offset: 0x000AF388
		public override string EncodingName
		{
			get
			{
				return this.hisCustomEncodingMaps.Name;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060034ED RID: 13549 RVA: 0x000B1188 File Offset: 0x000AF388
		public override string BodyName
		{
			get
			{
				return this.hisCustomEncodingMaps.Name;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060034EE RID: 13550 RVA: 0x000B1188 File Offset: 0x000AF388
		public override string HeaderName
		{
			get
			{
				return this.hisCustomEncodingMaps.Name;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x000B1188 File Offset: 0x000AF388
		public override string WebName
		{
			get
			{
				return this.hisCustomEncodingMaps.Name;
			}
		}
	}
}
