using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001C5 RID: 453
	internal abstract class XmlElementValue
	{
		// Token: 0x06000D07 RID: 3335 RVA: 0x000258DB File Offset: 0x00023ADB
		internal XmlElementValue(string elementName, CsdlLocation elementLocation)
		{
			this.Name = elementName;
			this.Location = elementLocation;
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x000258F1 File Offset: 0x00023AF1
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x000258F9 File Offset: 0x00023AF9
		internal string Name { get; private set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00025902 File Offset: 0x00023B02
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x0002590A File Offset: 0x00023B0A
		internal CsdlLocation Location { get; private set; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000D0C RID: 3340
		internal abstract object UntypedValue { get; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000D0D RID: 3341
		internal abstract bool IsUsed { get; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x000026A6 File Offset: 0x000008A6
		internal virtual bool IsText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00025913 File Offset: 0x00023B13
		internal virtual string TextValue
		{
			get
			{
				return this.ValueAs<string>();
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0002591B File Offset: 0x00023B1B
		internal virtual TValue ValueAs<TValue>() where TValue : class
		{
			return this.UntypedValue as TValue;
		}
	}
}
