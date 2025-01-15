using System;
using System.Globalization;

namespace dotless.Core.Plugins
{
	// Token: 0x02000020 RID: 32
	public class PluginParameter : IPluginParameter
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00003F2A File Offset: 0x0000212A
		public PluginParameter(string name, Type type, bool isMandatory)
		{
			this.Name = name;
			this.IsMandatory = isMandatory;
			this.Type = type;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003F47 File Offset: 0x00002147
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003F4F File Offset: 0x0000214F
		public string Name { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003F58 File Offset: 0x00002158
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003F60 File Offset: 0x00002160
		public bool IsMandatory { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003F69 File Offset: 0x00002169
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003F71 File Offset: 0x00002171
		public object Value { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003F7A File Offset: 0x0000217A
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003F82 File Offset: 0x00002182
		private Type Type { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003F8B File Offset: 0x0000218B
		public string TypeDescription
		{
			get
			{
				return this.Type.Name;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003F98 File Offset: 0x00002198
		public void SetValue(string stringValue)
		{
			if (!this.Type.Equals(typeof(bool)))
			{
				this.Value = Convert.ChangeType(stringValue, this.Type, CultureInfo.InvariantCulture);
				return;
			}
			if (stringValue.Equals("true", StringComparison.InvariantCultureIgnoreCase) || stringValue.Equals("t", StringComparison.InvariantCultureIgnoreCase) || stringValue.Equals("1", StringComparison.InvariantCultureIgnoreCase))
			{
				this.Value = true;
				return;
			}
			this.Value = false;
		}
	}
}
