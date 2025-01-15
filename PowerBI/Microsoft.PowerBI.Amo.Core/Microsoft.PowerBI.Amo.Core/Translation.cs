using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000C7 RID: 199
	[Guid("14E12A78-5BD7-41cd-815B-CC5826265866")]
	[Designer("Microsoft.AnalysisServices.Design.TranslationDesigner, Microsoft.AnalysisServices.Design.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91")]
	public class Translation : ModelComponent, ICloneable
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x00029835 File Offset: 0x00027A35
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00029840 File Offset: 0x00027A40
		protected internal override string FriendlyName
		{
			get
			{
				if (this.language <= 0)
				{
					return this.language.ToString(CultureInfo.InvariantCulture);
				}
				string text;
				try
				{
					text = CultureInfo.GetCultureInfo(this.language).DisplayName;
				}
				catch
				{
					text = this.language.ToString(CultureInfo.InvariantCulture);
				}
				return text;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x000298A0 File Offset: 0x00027AA0
		protected internal override string KeyForCollection
		{
			get
			{
				return this.Language.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x000298C0 File Offset: 0x00027AC0
		public Translation()
		{
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000298C8 File Offset: 0x00027AC8
		public Translation(int language)
		{
			this.Language = language;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x000298D7 File Offset: 0x00027AD7
		public Translation(int language, string caption)
		{
			this.Language = language;
			this.Caption = caption;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x000298ED File Offset: 0x00027AED
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x000298F5 File Offset: 0x00027AF5
		[ReadOnly(true)]
		[TypeConverter("Microsoft.AnalysisServices.Design.LanguageIdTypeConverter, Microsoft.AnalysisServices.Design.AS")]
		[LocalizedDescription("PropertyDescription_Translation_Language")]
		public int Language
		{
			get
			{
				return this.language;
			}
			set
			{
				if (value != this.language)
				{
					if (base.OwningCollection != null)
					{
						throw new InvalidOperationException(SR.PropertyCannotBeChangedForObjectInCollection("Language", typeof(Translation).Name));
					}
					this.language = value;
				}
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0002992E File Offset: 0x00027B2E
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x00029936 File Offset: 0x00027B36
		[XmlElement(IsNullable = false)]
		[LocalizedDescription("PropertyDescription_Translation_Caption")]
		public string Caption
		{
			get
			{
				return this.caption;
			}
			set
			{
				this.caption = Utils.Trim(value);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00029944 File Offset: 0x00027B44
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x0002994C File Offset: 0x00027B4C
		[XmlElement(IsNullable = false)]
		[LocalizedDescription("PropertyDescription_Translation_Description")]
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00029955 File Offset: 0x00027B55
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x0002995D File Offset: 0x00027B5D
		[XmlElement(IsNullable = false)]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[TypeConverter("Microsoft.AnalysisServices.Design.DisplayFolderConverter, Microsoft.AnalysisServices.Design.AS")]
		[LocalizedDescription("PropertyDescription_Translation_DisplayFolder")]
		public string DisplayFolder
		{
			get
			{
				return this.displayFolder;
			}
			set
			{
				this.displayFolder = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00029966 File Offset: 0x00027B66
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x0002996E File Offset: 0x00027B6E
		[XmlElement(IsNullable = false, Namespace = "http://schemas.microsoft.com/analysisservices/2011/engine/300")]
		public string CollectionCaption
		{
			get
			{
				return this.collectionCaption;
			}
			set
			{
				this.collectionCaption = value;
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00029978 File Offset: 0x00027B78
		public Translation CopyTo(Translation obj)
		{
			base.CopyTo(obj);
			obj.Language = this.Language;
			obj.Caption = this.Caption;
			obj.Description = this.Description;
			obj.DisplayFolder = this.DisplayFolder;
			obj.CollectionCaption = this.CollectionCaption;
			return obj;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000299C9 File Offset: 0x00027BC9
		public virtual Translation Clone()
		{
			return this.CopyTo(new Translation());
		}

		// Token: 0x040006F3 RID: 1779
		internal const int DefaultLanguage = 0;

		// Token: 0x040006F4 RID: 1780
		private int language;

		// Token: 0x040006F5 RID: 1781
		private string caption;

		// Token: 0x040006F6 RID: 1782
		private string description;

		// Token: 0x040006F7 RID: 1783
		private string displayFolder;

		// Token: 0x040006F8 RID: 1784
		private string collectionCaption;
	}
}
