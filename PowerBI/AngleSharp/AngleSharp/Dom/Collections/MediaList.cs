using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AngleSharp.Css;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x020003FC RID: 1020
	internal sealed class MediaList : CssNode, IMediaList, ICssNode, IStyleFormattable, IEnumerable<ICssMedium>, IEnumerable
	{
		// Token: 0x0600205B RID: 8283 RVA: 0x00056224 File Offset: 0x00054424
		internal MediaList(CssParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x17000A2F RID: 2607
		public string this[int index]
		{
			get
			{
				return this.Media.GetItemByIndex(index).ToCss();
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x00056246 File Offset: 0x00054446
		public IEnumerable<CssMedium> Media
		{
			get
			{
				return base.Children.OfType<CssMedium>();
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x00056253 File Offset: 0x00054453
		public int Length
		{
			get
			{
				return this.Media.Count<CssMedium>();
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x0004810B File Offset: 0x0004630B
		// (set) Token: 0x06002060 RID: 8288 RVA: 0x00056260 File Offset: 0x00054460
		public string MediaText
		{
			get
			{
				return this.ToCss();
			}
			set
			{
				base.Clear();
				foreach (CssMedium cssMedium in this._parser.ParseMediaList(value))
				{
					if (cssMedium == null)
					{
						throw new DomException(DomError.Syntax);
					}
					base.AppendChild(cssMedium);
				}
			}
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000562CC File Offset: 0x000544CC
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			CssMedium[] array = this.Media.ToArray<CssMedium>();
			if (array.Length != 0)
			{
				array[0].ToCss(writer, formatter);
				for (int i = 1; i < array.Length; i++)
				{
					writer.Write(", ");
					array[i].ToCss(writer, formatter);
				}
			}
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00056318 File Offset: 0x00054518
		public bool Validate(RenderDevice device)
		{
			return !this.Media.Any((CssMedium m) => !m.Validate(device));
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x0005634C File Offset: 0x0005454C
		public void Add(string newMedium)
		{
			CssMedium cssMedium = this._parser.ParseMedium(newMedium);
			if (cssMedium == null)
			{
				throw new DomException(DomError.Syntax);
			}
			base.AppendChild(cssMedium);
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00056378 File Offset: 0x00054578
		public void Remove(string oldMedium)
		{
			CssMedium cssMedium = this._parser.ParseMedium(oldMedium);
			if (cssMedium == null)
			{
				throw new DomException(DomError.Syntax);
			}
			foreach (CssMedium cssMedium2 in this.Media)
			{
				if (cssMedium2.Equals(cssMedium))
				{
					base.RemoveChild(cssMedium2);
					return;
				}
			}
			throw new DomException(DomError.NotFound);
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x000563F0 File Offset: 0x000545F0
		public IEnumerator<ICssMedium> GetEnumerator()
		{
			return this.Media.GetEnumerator();
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000563FD File Offset: 0x000545FD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000D11 RID: 3345
		private readonly CssParser _parser;
	}
}
