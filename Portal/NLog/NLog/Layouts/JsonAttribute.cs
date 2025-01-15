using System;
using NLog.Config;
using NLog.LayoutRenderers.Wrappers;

namespace NLog.Layouts
{
	// Token: 0x020000A4 RID: 164
	[NLogConfigurationItem]
	[ThreadAgnostic]
	[ThreadSafe]
	[AppDomainFixedOutput]
	public class JsonAttribute
	{
		// Token: 0x06000A84 RID: 2692 RVA: 0x0001B383 File Offset: 0x00019583
		public JsonAttribute()
			: this(null, null, true)
		{
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001B38E File Offset: 0x0001958E
		public JsonAttribute(string name, Layout layout)
			: this(name, layout, true)
		{
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001B399 File Offset: 0x00019599
		public JsonAttribute(string name, Layout layout, bool encode)
		{
			this.Name = name;
			this.Layout = layout;
			this.Encode = encode;
			this.IncludeEmptyValue = false;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0001B3C8 File Offset: 0x000195C8
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0001B3D0 File Offset: 0x000195D0
		[RequiredParameter]
		public string Name { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0001B3D9 File Offset: 0x000195D9
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0001B3E6 File Offset: 0x000195E6
		[RequiredParameter]
		public Layout Layout
		{
			get
			{
				return this.LayoutWrapper.Inner;
			}
			set
			{
				this.LayoutWrapper.Inner = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0001B3F4 File Offset: 0x000195F4
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x0001B401 File Offset: 0x00019601
		public bool Encode
		{
			get
			{
				return this.LayoutWrapper.JsonEncode;
			}
			set
			{
				this.LayoutWrapper.JsonEncode = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0001B40F File Offset: 0x0001960F
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x0001B41C File Offset: 0x0001961C
		public bool EscapeUnicode
		{
			get
			{
				return this.LayoutWrapper.EscapeUnicode;
			}
			set
			{
				this.LayoutWrapper.EscapeUnicode = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0001B42A File Offset: 0x0001962A
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x0001B432 File Offset: 0x00019632
		public bool IncludeEmptyValue { get; set; }

		// Token: 0x0400027D RID: 637
		internal readonly JsonEncodeLayoutRendererWrapper LayoutWrapper = new JsonEncodeLayoutRendererWrapper();
	}
}
