using System;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x0200004E RID: 78
	[AttributeUsage(AttributeTargets.All)]
	public class LocalizedNameAttribute : Attribute
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00002470 File Offset: 0x00000670
		public LocalizedNameAttribute()
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002478 File Offset: 0x00000678
		public LocalizedNameAttribute(string name)
		{
			this.m_name = name;
			this.m_localized = false;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002490 File Offset: 0x00000690
		public string Name
		{
			get
			{
				if (!this.m_localized)
				{
					this.m_localized = true;
					string localizedString = this.GetLocalizedString(this.m_name);
					if (localizedString != null)
					{
						this.m_name = localizedString;
					}
				}
				return this.m_name;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000024C9 File Offset: 0x000006C9
		public override bool Equals(object obj)
		{
			return obj == this || (obj is LocalizedNameAttribute && this.Name.Equals(((LocalizedNameAttribute)obj).Name));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000024F1 File Offset: 0x000006F1
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000024FE File Offset: 0x000006FE
		protected virtual string GetLocalizedString(string value)
		{
			return value;
		}

		// Token: 0x04000229 RID: 553
		private string m_name;

		// Token: 0x0400022A RID: 554
		private bool m_localized;
	}
}
