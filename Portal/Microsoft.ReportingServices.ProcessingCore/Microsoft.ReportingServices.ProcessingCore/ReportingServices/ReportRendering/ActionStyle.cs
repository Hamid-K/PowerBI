using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000022 RID: 34
	public sealed class ActionStyle : StyleBase
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x0000B6D6 File Offset: 0x000098D6
		public ActionStyle()
		{
			Global.Tracer.Assert(base.IsCustomControl);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000B6EE File Offset: 0x000098EE
		internal ActionStyle(ActionInfo actionInfo, RenderingContext context)
			: base(context)
		{
			Global.Tracer.Assert(!base.IsCustomControl);
			this.m_actionInfo = actionInfo;
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000B711 File Offset: 0x00009911
		public override int Count
		{
			get
			{
				if (base.IsCustomControl)
				{
					return base.Count;
				}
				if (this.m_actionInfo.ActionInfoDef.StyleClass == null)
				{
					return 0;
				}
				return base.Count;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000B73C File Offset: 0x0000993C
		public override ICollection Keys
		{
			get
			{
				if (base.IsCustomControl)
				{
					return base.Keys;
				}
				if (this.m_actionInfo.ActionInfoDef.StyleClass == null)
				{
					return null;
				}
				return base.Keys;
			}
		}

		// Token: 0x17000360 RID: 864
		public override object this[string styleName]
		{
			get
			{
				if (base.IsCustomControl)
				{
					object obj = null;
					if (this.m_nonSharedProperties != null)
					{
						obj = this.m_nonSharedProperties[styleName];
					}
					if (obj == null && this.m_sharedProperties != null)
					{
						obj = this.m_sharedProperties[styleName];
					}
					return this.CreateProperty(styleName, obj);
				}
				Global.Tracer.Assert(!base.IsCustomControl);
				if (this.m_actionInfo.ActionInfoDef.StyleClass == null)
				{
					return null;
				}
				StyleAttributeHashtable styleAttributes = this.m_actionInfo.ActionInfoDef.StyleClass.StyleAttributes;
				if ("BackgroundImage" == styleName)
				{
					Image.SourceType sourceType = Image.SourceType.External;
					object obj2 = null;
					object obj3 = null;
					bool flag = false;
					bool flag2;
					base.GetBackgroundImageProperties(styleAttributes["BackgroundImageSource"], styleAttributes["BackgroundImageValue"], styleAttributes["BackgroundImageMIMEType"], out sourceType, out obj2, out flag2, out obj3, out flag);
					if (obj2 != null)
					{
						string text = null;
						if (!flag)
						{
							text = (string)obj3;
						}
						return new BackgroundImage(this.m_renderingContext, sourceType, obj2, text);
					}
				}
				else
				{
					AttributeInfo attributeInfo = styleAttributes[styleName];
					if (attributeInfo != null)
					{
						return this.CreateProperty(styleName, this.GetStyleAttributeValue(styleName, attributeInfo));
					}
				}
				return null;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000B87F File Offset: 0x00009A7F
		public override StyleProperties SharedProperties
		{
			get
			{
				if (base.IsCustomControl)
				{
					return this.m_sharedProperties;
				}
				if (this.NeedPopulateSharedProps())
				{
					this.PopulateStyleProperties(false);
					this.m_actionInfo.ActionInfoDef.SharedStyleProperties = this.m_sharedProperties;
				}
				return this.m_sharedProperties;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000B8BC File Offset: 0x00009ABC
		public override StyleProperties NonSharedProperties
		{
			get
			{
				if (base.IsCustomControl)
				{
					return this.m_nonSharedProperties;
				}
				if (this.NeedPopulateNonSharedProps())
				{
					this.PopulateNonSharedStyleProperties();
					if (this.m_nonSharedProperties == null || this.m_nonSharedProperties.Count == 0)
					{
						this.m_actionInfo.ActionInfoDef.NoNonSharedStyleProps = true;
					}
				}
				return this.m_nonSharedProperties;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000B912 File Offset: 0x00009B12
		private bool NeedPopulateSharedProps()
		{
			if (this.m_sharedProperties != null)
			{
				return false;
			}
			if (this.m_actionInfo.ActionInfoDef.SharedStyleProperties != null)
			{
				this.m_sharedProperties = this.m_actionInfo.ActionInfoDef.SharedStyleProperties;
				return false;
			}
			return true;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000B949 File Offset: 0x00009B49
		private bool NeedPopulateNonSharedProps()
		{
			return this.m_nonSharedProperties == null && !this.m_actionInfo.ActionInfoDef.NoNonSharedStyleProps;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000B968 File Offset: 0x00009B68
		internal override object GetStyleAttributeValue(string styleName, AttributeInfo attribute)
		{
			Global.Tracer.Assert(!base.IsCustomControl);
			if (attribute.IsExpression)
			{
				ActionInstance actionInfoInstance = this.m_actionInfo.ActionInfoInstance;
				if (actionInfoInstance != null)
				{
					return actionInfoInstance.GetStyleAttributeValue(attribute.IntValue);
				}
				return null;
			}
			else
			{
				if ("NumeralVariant" == styleName)
				{
					return attribute.IntValue;
				}
				return attribute.Value;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		internal override void PopulateStyleProperties(bool populateAll)
		{
			if (base.IsCustomControl)
			{
				return;
			}
			bool flag = true;
			bool flag2 = false;
			if (populateAll)
			{
				flag = this.NeedPopulateSharedProps();
				flag2 = this.NeedPopulateNonSharedProps();
				if (!flag && !flag2)
				{
					return;
				}
			}
			Style styleClass = this.m_actionInfo.ActionInfoDef.StyleClass;
			StyleAttributeHashtable styleAttributeHashtable = null;
			if (styleClass != null)
			{
				styleAttributeHashtable = styleClass.StyleAttributes;
			}
			Global.Tracer.Assert(styleAttributeHashtable != null);
			IDictionaryEnumerator enumerator = styleAttributeHashtable.GetEnumerator();
			while (enumerator.MoveNext())
			{
				AttributeInfo attributeInfo = (AttributeInfo)enumerator.Value;
				string text = (string)enumerator.Key;
				if ("BackgroundImage" == text)
				{
					Image.SourceType sourceType = Image.SourceType.External;
					object obj = null;
					object obj2 = null;
					bool flag3 = false;
					bool flag4 = false;
					base.GetBackgroundImageProperties(styleAttributeHashtable["BackgroundImageSource"], styleAttributeHashtable["BackgroundImageValue"], styleAttributeHashtable["BackgroundImageMIMEType"], out sourceType, out obj, out flag3, out obj2, out flag4);
					if (obj != null)
					{
						string text2 = null;
						if (!flag4)
						{
							text2 = (string)obj2;
						}
						object obj3 = new BackgroundImage(this.m_renderingContext, sourceType, obj, text2);
						base.AddStyleProperty(text, flag3 || flag4, flag2, flag, obj3);
					}
				}
				else if (!("BackgroundImageValue" == text) && !("BackgroundImageMIMEType" == text))
				{
					base.AddStyleProperty(text, attributeInfo.IsExpression, flag2, flag, this.CreateProperty(text, this.GetStyleAttributeValue(text, attributeInfo)));
				}
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000BB2C File Offset: 0x00009D2C
		private void PopulateNonSharedStyleProperties()
		{
			if (base.IsCustomControl)
			{
				return;
			}
			Style styleClass = this.m_actionInfo.ActionInfoDef.StyleClass;
			if (styleClass != null)
			{
				StyleAttributeHashtable styleAttributes = styleClass.StyleAttributes;
				Global.Tracer.Assert(styleAttributes != null);
				this.InternalPopulateNonSharedStyleProperties(styleAttributes);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000BB74 File Offset: 0x00009D74
		private void InternalPopulateNonSharedStyleProperties(StyleAttributeHashtable styleAttributes)
		{
			if (styleAttributes == null)
			{
				return;
			}
			IDictionaryEnumerator enumerator = styleAttributes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				AttributeInfo attributeInfo = (AttributeInfo)enumerator.Value;
				string text = (string)enumerator.Key;
				if ("BackgroundImageSource" == text)
				{
					Image.SourceType sourceType;
					object obj;
					bool flag;
					object obj2;
					bool flag2;
					if (base.GetBackgroundImageProperties(attributeInfo, styleAttributes["BackgroundImageValue"], styleAttributes["BackgroundImageMIMEType"], out sourceType, out obj, out flag, out obj2, out flag2) && (flag || flag2) && obj != null)
					{
						string text2 = null;
						if (!flag2)
						{
							text2 = (string)obj2;
						}
						object obj3 = new BackgroundImage(this.m_renderingContext, sourceType, obj, text2);
						base.SetStyleProperty("BackgroundImage", true, true, false, obj3);
					}
				}
				else if (!("BackgroundImageValue" == text) && !("BackgroundImageMIMEType" == text) && attributeInfo.IsExpression)
				{
					base.SetStyleProperty(text, true, true, false, this.CreateProperty(text, this.GetStyleAttributeValue(text, attributeInfo)));
				}
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000BC64 File Offset: 0x00009E64
		private object CreateProperty(string styleName, object styleValue)
		{
			if (styleValue == null)
			{
				return null;
			}
			return StyleBase.CreateStyleProperty(styleName, styleValue);
		}

		// Token: 0x040000B8 RID: 184
		private ActionInfo m_actionInfo;
	}
}
