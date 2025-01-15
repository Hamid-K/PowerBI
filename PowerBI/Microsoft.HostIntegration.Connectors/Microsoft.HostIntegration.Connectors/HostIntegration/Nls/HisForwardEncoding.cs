using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x0200062F RID: 1583
	internal class HisForwardEncoding : HisEncoding
	{
		// Token: 0x0600355D RID: 13661 RVA: 0x000B1787 File Offset: 0x000AF987
		internal HisForwardEncoding(HisEncoding.HostCodePages hostCP)
			: base(hostCP)
		{
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000B3530 File Offset: 0x000B1730
		internal HisForwardEncoding(Encoding forward)
			: base((HisEncoding.HostCodePages)forward.CodePage)
		{
			this.forwardEncoding = forward;
			this.converter = null;
			this.destinationCodePage = (HisEncoding.HostCodePages)this.forwardEncoding.CodePage;
			if (!HisForwardEncoding.failedCodePages.Contains(this.forwardEncoding.CodePage))
			{
				try
				{
					this.windowsCodePage = (HisEncoding.WindowsCodePages)this.forwardEncoding.WindowsCodePage;
				}
				catch (Exception)
				{
					HisForwardEncoding.failedCodePages.Add(this.forwardEncoding.CodePage);
				}
			}
			this.windowsEncoding = Encoding.GetEncoding((int)this.windowsCodePage);
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000B35CC File Offset: 0x000B17CC
		internal override void Intialize(HisEncoding.HostCodePages hostCP)
		{
			if (this.forwardEncoding == null)
			{
				this.forwardEncoding = Encoding.GetEncoding((int)hostCP);
				this.converter = null;
				this.destinationCodePage = (HisEncoding.HostCodePages)this.forwardEncoding.CodePage;
				if (!HisForwardEncoding.failedCodePages.Contains(this.forwardEncoding.CodePage))
				{
					try
					{
						this.windowsCodePage = (HisEncoding.WindowsCodePages)this.forwardEncoding.WindowsCodePage;
					}
					catch (Exception)
					{
						HisForwardEncoding.failedCodePages.Add(this.forwardEncoding.CodePage);
					}
				}
				this.windowsEncoding = Encoding.GetEncoding((int)this.windowsCodePage);
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x000B366C File Offset: 0x000B186C
		public override string BodyName
		{
			get
			{
				return this.forwardEncoding.BodyName;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x000B3679 File Offset: 0x000B1879
		public override int CodePage
		{
			get
			{
				return this.forwardEncoding.CodePage;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x000B3686 File Offset: 0x000B1886
		public override string EncodingName
		{
			get
			{
				return this.forwardEncoding.EncodingName;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06003563 RID: 13667 RVA: 0x000B3693 File Offset: 0x000B1893
		public override string HeaderName
		{
			get
			{
				return this.forwardEncoding.HeaderName;
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x000B36A0 File Offset: 0x000B18A0
		public override bool IsBrowserDisplay
		{
			get
			{
				return this.forwardEncoding.IsBrowserDisplay;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06003565 RID: 13669 RVA: 0x000B36AD File Offset: 0x000B18AD
		public override bool IsBrowserSave
		{
			get
			{
				return this.forwardEncoding.IsBrowserSave;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06003566 RID: 13670 RVA: 0x000B36BA File Offset: 0x000B18BA
		public override bool IsMailNewsDisplay
		{
			get
			{
				return this.forwardEncoding.IsMailNewsDisplay;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06003567 RID: 13671 RVA: 0x000B36C7 File Offset: 0x000B18C7
		public override bool IsMailNewsSave
		{
			get
			{
				return this.forwardEncoding.IsMailNewsSave;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06003568 RID: 13672 RVA: 0x000B36D4 File Offset: 0x000B18D4
		public new bool IsReadOnly
		{
			get
			{
				return this.forwardEncoding.IsReadOnly;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06003569 RID: 13673 RVA: 0x000B36E1 File Offset: 0x000B18E1
		public override bool IsSingleByte
		{
			get
			{
				return this.forwardEncoding.IsSingleByte;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600356A RID: 13674 RVA: 0x000B36EE File Offset: 0x000B18EE
		public override string WebName
		{
			get
			{
				return this.forwardEncoding.WebName;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x0600356B RID: 13675 RVA: 0x000B36FB File Offset: 0x000B18FB
		public override int WindowsCodePage
		{
			get
			{
				return this.forwardEncoding.WindowsCodePage;
			}
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000B3708 File Offset: 0x000B1908
		public override int GetByteCount(char[] chars)
		{
			return this.forwardEncoding.GetByteCount(chars);
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000B3716 File Offset: 0x000B1916
		public override int GetByteCount(string s)
		{
			return this.forwardEncoding.GetByteCount(s);
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000B3724 File Offset: 0x000B1924
		[SecurityCritical]
		[ComVisible(false)]
		public unsafe override int GetByteCount(char* chars, int count)
		{
			return this.forwardEncoding.GetByteCount(chars, count);
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000B3733 File Offset: 0x000B1933
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return this.forwardEncoding.GetByteCount(chars, index, count);
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000B3743 File Offset: 0x000B1943
		public override byte[] GetBytes(char[] chars)
		{
			return this.forwardEncoding.GetBytes(chars);
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000B3751 File Offset: 0x000B1951
		public override byte[] GetBytes(string s)
		{
			return this.forwardEncoding.GetBytes(s);
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000B375F File Offset: 0x000B195F
		public override byte[] GetBytes(char[] chars, int index, int count)
		{
			return this.forwardEncoding.GetBytes(chars, index, count);
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000B376F File Offset: 0x000B196F
		[ComVisible(false)]
		[SecurityCritical]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			return this.forwardEncoding.GetBytes(chars, charCount, bytes, byteCount);
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000B3781 File Offset: 0x000B1981
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return this.forwardEncoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000B3795 File Offset: 0x000B1995
		public override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return this.forwardEncoding.GetBytes(s, charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000B37A9 File Offset: 0x000B19A9
		public override int GetCharCount(byte[] bytes)
		{
			return this.forwardEncoding.GetCharCount(bytes);
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000B37B7 File Offset: 0x000B19B7
		[SecurityCritical]
		[ComVisible(false)]
		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			return this.forwardEncoding.GetCharCount(bytes, count);
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x000B37C6 File Offset: 0x000B19C6
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.forwardEncoding.GetCharCount(bytes, index, count);
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x000B37D6 File Offset: 0x000B19D6
		public override char[] GetChars(byte[] bytes)
		{
			return this.forwardEncoding.GetChars(bytes);
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x000B37E4 File Offset: 0x000B19E4
		public override char[] GetChars(byte[] bytes, int index, int count)
		{
			return this.forwardEncoding.GetChars(bytes, index, count);
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000B37F4 File Offset: 0x000B19F4
		[ComVisible(false)]
		[SecurityCritical]
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			return this.forwardEncoding.GetChars(bytes, byteCount, chars, charCount);
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000B3806 File Offset: 0x000B1A06
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.forwardEncoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000B381A File Offset: 0x000B1A1A
		public override Decoder GetDecoder()
		{
			return this.forwardEncoding.GetDecoder();
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000B3827 File Offset: 0x000B1A27
		public override Encoder GetEncoder()
		{
			return this.forwardEncoding.GetEncoder();
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000B3834 File Offset: 0x000B1A34
		public override int GetHashCode()
		{
			return this.forwardEncoding.GetHashCode();
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000B3841 File Offset: 0x000B1A41
		public override int GetMaxByteCount(int charCount)
		{
			return this.forwardEncoding.GetMaxByteCount(charCount);
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000B384F File Offset: 0x000B1A4F
		public override int GetMaxCharCount(int byteCount)
		{
			return this.forwardEncoding.GetMaxCharCount(byteCount);
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000B385D File Offset: 0x000B1A5D
		public override byte[] GetPreamble()
		{
			return this.forwardEncoding.GetPreamble();
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000B386A File Offset: 0x000B1A6A
		public override string GetString(byte[] bytes)
		{
			return this.forwardEncoding.GetString(bytes);
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x000B3878 File Offset: 0x000B1A78
		public override string GetString(byte[] bytes, int index, int count)
		{
			return this.forwardEncoding.GetString(bytes, index, count);
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000B3888 File Offset: 0x000B1A88
		[ComVisible(false)]
		public new bool IsAlwaysNormalized()
		{
			return this.forwardEncoding.IsAlwaysNormalized();
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x000B3895 File Offset: 0x000B1A95
		[ComVisible(false)]
		public override bool IsAlwaysNormalized(NormalizationForm form)
		{
			return this.forwardEncoding.IsAlwaysNormalized(form);
		}

		// Token: 0x04001E95 RID: 7829
		private Encoding forwardEncoding;

		// Token: 0x04001E96 RID: 7830
		private static HashSet<int> failedCodePages = new HashSet<int>();
	}
}
