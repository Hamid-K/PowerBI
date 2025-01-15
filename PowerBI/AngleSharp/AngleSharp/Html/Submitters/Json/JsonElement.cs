using System;

namespace AngleSharp.Html.Submitters.Json
{
	// Token: 0x020000C8 RID: 200
	internal abstract class JsonElement
	{
		// Token: 0x17000110 RID: 272
		public virtual JsonElement this[string key]
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}
	}
}
