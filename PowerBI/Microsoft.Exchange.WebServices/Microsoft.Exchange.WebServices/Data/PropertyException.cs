using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200025F RID: 607
	[Serializable]
	public class PropertyException : ServiceLocalException
	{
		// Token: 0x060015B0 RID: 5552 RVA: 0x0003CD80 File Offset: 0x0003BD80
		public PropertyException(string name)
		{
			this.name = name;
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0003CD8F File Offset: 0x0003BD8F
		public PropertyException(string message, string name)
			: base(message)
		{
			this.name = name;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0003CD9F File Offset: 0x0003BD9F
		public PropertyException(string message, string name, Exception innerException)
			: base(message, innerException)
		{
			this.name = name;
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x0003CDB0 File Offset: 0x0003BDB0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040012B3 RID: 4787
		private string name;
	}
}
