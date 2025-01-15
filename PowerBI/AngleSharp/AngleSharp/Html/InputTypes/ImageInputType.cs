using System;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Network.RequestProcessors;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000D8 RID: 216
	internal class ImageInputType : BaseInputType
	{
		// Token: 0x06000652 RID: 1618 RVA: 0x0003059C File Offset: 0x0002E79C
		public ImageInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
			HtmlInputElement htmlInputElement = input as HtmlInputElement;
			string source = input.Source;
			if (source != null && htmlInputElement != null)
			{
				Url url = htmlInputElement.HyperReference(source);
				this._request = ImageRequestProcessor.Create(htmlInputElement);
				htmlInputElement.Process(this._request, url);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x000305E7 File Offset: 0x0002E7E7
		public int Width
		{
			get
			{
				if (this._request == null)
				{
					return 0;
				}
				return this._request.Width;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x000305FE File Offset: 0x0002E7FE
		public int Height
		{
			get
			{
				if (this._request == null)
				{
					return 0;
				}
				return this._request.Height;
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00030615 File Offset: 0x0002E815
		public override bool IsAppendingData(IHtmlElement submitter)
		{
			return submitter == base.Input && !string.IsNullOrEmpty(base.Input.Name);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00030638 File Offset: 0x0002E838
		public override void ConstructDataSet(FormDataSet dataSet)
		{
			string name = base.Input.Name;
			string value = base.Input.Value;
			dataSet.Append(name + ".x", "0", base.Input.Type);
			dataSet.Append(name + ".y", "0", base.Input.Type);
			if (!string.IsNullOrEmpty(value))
			{
				dataSet.Append(name, value, base.Input.Type);
			}
		}

		// Token: 0x04000602 RID: 1538
		private readonly ImageRequestProcessor _request;
	}
}
