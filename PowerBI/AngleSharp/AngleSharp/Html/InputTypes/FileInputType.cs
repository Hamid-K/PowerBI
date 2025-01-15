using System;
using AngleSharp.Dom.Html;
using AngleSharp.Dom.Io;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000D7 RID: 215
	internal class FileInputType : BaseInputType
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x000304EE File Offset: 0x0002E6EE
		public FileInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
			this._files = new FileList();
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00030504 File Offset: 0x0002E704
		public FileList Files
		{
			get
			{
				return this._files;
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0003050C File Offset: 0x0002E70C
		public override void ConstructDataSet(FormDataSet dataSet)
		{
			if (this._files.Length == 0)
			{
				dataSet.Append(base.Input.Name, null, base.Input.Type);
			}
			foreach (IFile file in this._files)
			{
				dataSet.Append(base.Input.Name, file, base.Input.Type);
			}
		}

		// Token: 0x04000601 RID: 1537
		private readonly FileList _files;
	}
}
