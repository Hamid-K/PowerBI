using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000B4 RID: 180
	internal sealed class Style
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x000141FD File Offset: 0x000123FD
		internal Style()
		{
			this._propertyValues = new Dictionary<string, string>();
			this._onceFlag = false;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00014217 File Offset: 0x00012417
		internal Style(Dictionary<string, string> propertyValues)
		{
			this._propertyValues = propertyValues;
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00014226 File Offset: 0x00012426
		public Dictionary<string, string> PropertyValues
		{
			get
			{
				return this._propertyValues;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0001422E File Offset: 0x0001242E
		public int Count
		{
			get
			{
				return this._propertyValues.Count;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0001423B File Offset: 0x0001243B
		public TextStyleInfo TextStyleInfo
		{
			get
			{
				if (!this._onceFlag)
				{
					this._textStyleInfo = new TextStyleInfo(this);
					this._onceFlag = true;
				}
				return this._textStyleInfo;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001425E File Offset: 0x0001245E
		public Style Combine(Style newStyle)
		{
			new Dictionary<string, string>(newStyle.PropertyValues).ToList<KeyValuePair<string, string>>().ForEach(delegate(KeyValuePair<string, string> x)
			{
				this._propertyValues[x.Key] = x.Value;
			});
			return new Style(this._propertyValues);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001428C File Offset: 0x0001248C
		public bool HasProperty(string propertyInfo)
		{
			return this._propertyValues.Keys.Contains(propertyInfo);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001429F File Offset: 0x0001249F
		public string GetPropertyValue(string propertyInfo)
		{
			if (this.HasProperty(propertyInfo))
			{
				return this._propertyValues[propertyInfo];
			}
			return null;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000142B8 File Offset: 0x000124B8
		public void SetPropertyValue(string propertyInfo, string value)
		{
			this._propertyValues[propertyInfo] = value;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000142C8 File Offset: 0x000124C8
		public void SetPropertyValues(Dictionary<string, string> bag)
		{
			Dictionary<string, string> propertyValues = this._propertyValues;
			foreach (KeyValuePair<string, string> keyValuePair in bag)
			{
				propertyValues[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x0400024E RID: 590
		private Dictionary<string, string> _propertyValues;

		// Token: 0x0400024F RID: 591
		private bool _onceFlag;

		// Token: 0x04000250 RID: 592
		private TextStyleInfo _textStyleInfo;
	}
}
