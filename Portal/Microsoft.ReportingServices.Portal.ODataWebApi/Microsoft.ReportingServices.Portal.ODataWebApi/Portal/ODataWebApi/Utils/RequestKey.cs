using System;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Utils
{
	// Token: 0x02000038 RID: 56
	public class RequestKey
	{
		// Token: 0x060002CB RID: 715 RVA: 0x0000B6D9 File Offset: 0x000098D9
		public RequestKey(string key)
		{
			this.ParseRequest(key);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000B6E8 File Offset: 0x000098E8
		public bool IsPath
		{
			get
			{
				return this._isPath;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000B6F8 File Offset: 0x000098F8
		internal void ParseRequest(string key)
		{
			this._path = "Path='";
			this._isPath = false;
			if (key.StartsWith(this._path, StringComparison.OrdinalIgnoreCase))
			{
				this._isPath = true;
				this._path = key.Substring(this._path.Length, key.Length - this._path.Length - 1);
				this._path = this._path.Replace("''", "'");
				return;
			}
			if (!Guid.TryParse(key, out this._id) && key.StartsWith("'/"))
			{
				this._path = key.Substring(1, key.Length - 2);
				this._isPath = true;
				this._path = this._path.Replace("''", "'");
			}
		}

		// Token: 0x040000A5 RID: 165
		private bool _isPath;

		// Token: 0x040000A6 RID: 166
		private string _path;

		// Token: 0x040000A7 RID: 167
		private Guid _id;
	}
}
