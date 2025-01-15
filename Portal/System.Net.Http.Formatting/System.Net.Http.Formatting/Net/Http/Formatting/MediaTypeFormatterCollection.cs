using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000045 RID: 69
	public class MediaTypeFormatterCollection : Collection<MediaTypeFormatter>
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x000095AE File Offset: 0x000077AE
		public MediaTypeFormatterCollection()
			: this(MediaTypeFormatterCollection.CreateDefaultFormatters())
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000095BB File Offset: 0x000077BB
		public MediaTypeFormatterCollection(IEnumerable<MediaTypeFormatter> formatters)
		{
			this.VerifyAndSetFormatters(formatters);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002B4 RID: 692 RVA: 0x000095CC File Offset: 0x000077CC
		// (remove) Token: 0x060002B5 RID: 693 RVA: 0x00009604 File Offset: 0x00007804
		internal event EventHandler Changing;

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00009639 File Offset: 0x00007839
		public XmlMediaTypeFormatter XmlFormatter
		{
			get
			{
				return base.Items.OfType<XmlMediaTypeFormatter>().FirstOrDefault<XmlMediaTypeFormatter>();
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000964B File Offset: 0x0000784B
		public JsonMediaTypeFormatter JsonFormatter
		{
			get
			{
				return base.Items.OfType<JsonMediaTypeFormatter>().FirstOrDefault<JsonMediaTypeFormatter>();
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000965D File Offset: 0x0000785D
		public FormUrlEncodedMediaTypeFormatter FormUrlEncodedFormatter
		{
			get
			{
				return base.Items.OfType<FormUrlEncodedMediaTypeFormatter>().FirstOrDefault<FormUrlEncodedMediaTypeFormatter>();
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000966F File Offset: 0x0000786F
		internal MediaTypeFormatter[] WritingFormatters
		{
			get
			{
				if (this._writingFormatters == null)
				{
					this._writingFormatters = this.GetWritingFormatters();
				}
				return this._writingFormatters;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000968C File Offset: 0x0000788C
		public void AddRange(IEnumerable<MediaTypeFormatter> items)
		{
			if (items == null)
			{
				throw Error.ArgumentNull("items");
			}
			foreach (MediaTypeFormatter mediaTypeFormatter in items)
			{
				base.Add(mediaTypeFormatter);
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000096E4 File Offset: 0x000078E4
		public void InsertRange(int index, IEnumerable<MediaTypeFormatter> items)
		{
			if (items == null)
			{
				throw Error.ArgumentNull("items");
			}
			foreach (MediaTypeFormatter mediaTypeFormatter in items)
			{
				base.Insert(index++, mediaTypeFormatter);
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00009740 File Offset: 0x00007940
		public MediaTypeFormatter FindReader(Type type, MediaTypeHeaderValue mediaType)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (mediaType == null)
			{
				throw Error.ArgumentNull("mediaType");
			}
			foreach (MediaTypeFormatter mediaTypeFormatter in base.Items)
			{
				if (mediaTypeFormatter != null && mediaTypeFormatter.CanReadType(type))
				{
					foreach (MediaTypeHeaderValue mediaTypeHeaderValue in mediaTypeFormatter.SupportedMediaTypes)
					{
						if (mediaTypeHeaderValue != null && mediaTypeHeaderValue.IsSubsetOf(mediaType))
						{
							return mediaTypeFormatter;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00009800 File Offset: 0x00007A00
		public MediaTypeFormatter FindWriter(Type type, MediaTypeHeaderValue mediaType)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (mediaType == null)
			{
				throw Error.ArgumentNull("mediaType");
			}
			foreach (MediaTypeFormatter mediaTypeFormatter in base.Items)
			{
				if (mediaTypeFormatter != null && mediaTypeFormatter.CanWriteType(type))
				{
					foreach (MediaTypeHeaderValue mediaTypeHeaderValue in mediaTypeFormatter.SupportedMediaTypes)
					{
						if (mediaTypeHeaderValue != null && mediaTypeHeaderValue.IsSubsetOf(mediaType))
						{
							return mediaTypeFormatter;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000098C0 File Offset: 0x00007AC0
		public static bool IsTypeExcludedFromValidation(Type type)
		{
			return typeof(XmlNode).IsAssignableFrom(type) || typeof(FormDataCollection).IsAssignableFrom(type) || FormattingUtilities.IsJTokenType(type) || typeof(XObject).IsAssignableFrom(type) || typeof(Type).IsAssignableFrom(type) || type == typeof(byte[]);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000992F File Offset: 0x00007B2F
		protected override void ClearItems()
		{
			this.OnChanging();
			base.ClearItems();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000993D File Offset: 0x00007B3D
		protected override void InsertItem(int index, MediaTypeFormatter item)
		{
			this.OnChanging();
			base.InsertItem(index, item);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000994D File Offset: 0x00007B4D
		protected override void RemoveItem(int index)
		{
			this.OnChanging();
			base.RemoveItem(index);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000995C File Offset: 0x00007B5C
		protected override void SetItem(int index, MediaTypeFormatter item)
		{
			this.OnChanging();
			base.SetItem(index, item);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000996C File Offset: 0x00007B6C
		private void OnChanging()
		{
			if (this.Changing != null)
			{
				this.Changing(this, EventArgs.Empty);
			}
			this._writingFormatters = null;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000998E File Offset: 0x00007B8E
		private MediaTypeFormatter[] GetWritingFormatters()
		{
			return base.Items.Where((MediaTypeFormatter formatter) => formatter != null && formatter.CanWriteAnyTypes).ToArray<MediaTypeFormatter>();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000099BF File Offset: 0x00007BBF
		private static IEnumerable<MediaTypeFormatter> CreateDefaultFormatters()
		{
			return new MediaTypeFormatter[]
			{
				new JsonMediaTypeFormatter(),
				new XmlMediaTypeFormatter(),
				new FormUrlEncodedMediaTypeFormatter()
			};
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000099E0 File Offset: 0x00007BE0
		private void VerifyAndSetFormatters(IEnumerable<MediaTypeFormatter> formatters)
		{
			if (formatters == null)
			{
				throw Error.ArgumentNull("formatters");
			}
			foreach (MediaTypeFormatter mediaTypeFormatter in formatters)
			{
				if (mediaTypeFormatter == null)
				{
					throw Error.Argument("formatters", Resources.CannotHaveNullInList, new object[] { MediaTypeFormatterCollection._mediaTypeFormatterType.Name });
				}
				base.Add(mediaTypeFormatter);
			}
		}

		// Token: 0x040000CB RID: 203
		private static readonly Type _mediaTypeFormatterType = typeof(MediaTypeFormatter);

		// Token: 0x040000CC RID: 204
		private MediaTypeFormatter[] _writingFormatters;
	}
}
