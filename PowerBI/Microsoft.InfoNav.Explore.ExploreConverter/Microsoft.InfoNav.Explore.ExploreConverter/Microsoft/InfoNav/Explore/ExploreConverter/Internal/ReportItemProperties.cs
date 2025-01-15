using System;
using System.Xml.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200009B RID: 155
	internal class ReportItemProperties
	{
		// Token: 0x060002FF RID: 767 RVA: 0x0000CFB3 File Offset: 0x0000B1B3
		internal ReportItemProperties()
		{
			this._zIndex = 0;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000CFC2 File Offset: 0x0000B1C2
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000CFCA File Offset: 0x0000B1CA
		internal Size Size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000CFD3 File Offset: 0x0000B1D3
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000CFDB File Offset: 0x0000B1DB
		internal ReportSize Top
		{
			get
			{
				return this._top;
			}
			set
			{
				this._top = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000CFE4 File Offset: 0x0000B1E4
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		internal ReportSize Left
		{
			get
			{
				return this._left;
			}
			set
			{
				this._left = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000CFF5 File Offset: 0x0000B1F5
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000CFFD File Offset: 0x0000B1FD
		public XElement Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this._style = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000D006 File Offset: 0x0000B206
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000D00E File Offset: 0x0000B20E
		public int ZIndex
		{
			get
			{
				return this._zIndex;
			}
			set
			{
				this._zIndex = value;
			}
		}

		// Token: 0x040001FF RID: 511
		private Size _size;

		// Token: 0x04000200 RID: 512
		private ReportSize _top;

		// Token: 0x04000201 RID: 513
		private ReportSize _left;

		// Token: 0x04000202 RID: 514
		private int _zIndex;

		// Token: 0x04000203 RID: 515
		private XElement _style;
	}
}
