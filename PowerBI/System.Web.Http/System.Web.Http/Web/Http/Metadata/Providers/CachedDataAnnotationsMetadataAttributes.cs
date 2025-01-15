using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace System.Web.Http.Metadata.Providers
{
	// Token: 0x0200004A RID: 74
	public class CachedDataAnnotationsMetadataAttributes
	{
		// Token: 0x06000203 RID: 515 RVA: 0x00006678 File Offset: 0x00004878
		public CachedDataAnnotationsMetadataAttributes(IEnumerable<Attribute> attributes)
		{
			this.Display = attributes.OfType<DisplayAttribute>().FirstOrDefault<DisplayAttribute>();
			this.DisplayFormat = attributes.OfType<DisplayFormatAttribute>().FirstOrDefault<DisplayFormatAttribute>();
			this.DisplayName = attributes.OfType<DisplayNameAttribute>().FirstOrDefault<DisplayNameAttribute>();
			this.Editable = attributes.OfType<EditableAttribute>().FirstOrDefault<EditableAttribute>();
			this.ReadOnly = attributes.OfType<ReadOnlyAttribute>().FirstOrDefault<ReadOnlyAttribute>();
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000066E0 File Offset: 0x000048E0
		// (set) Token: 0x06000205 RID: 517 RVA: 0x000066E8 File Offset: 0x000048E8
		public DisplayAttribute Display { get; protected set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000066F1 File Offset: 0x000048F1
		// (set) Token: 0x06000207 RID: 519 RVA: 0x000066F9 File Offset: 0x000048F9
		public DisplayNameAttribute DisplayName { get; protected set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00006702 File Offset: 0x00004902
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000670A File Offset: 0x0000490A
		public DisplayFormatAttribute DisplayFormat { get; protected set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00006713 File Offset: 0x00004913
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000671B File Offset: 0x0000491B
		public EditableAttribute Editable { get; protected set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00006724 File Offset: 0x00004924
		// (set) Token: 0x0600020D RID: 525 RVA: 0x0000672C File Offset: 0x0000492C
		public ReadOnlyAttribute ReadOnly { get; protected set; }
	}
}
