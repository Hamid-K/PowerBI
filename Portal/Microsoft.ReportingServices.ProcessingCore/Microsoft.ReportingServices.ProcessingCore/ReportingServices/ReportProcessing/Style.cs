using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006D3 RID: 1747
	[Serializable]
	public sealed class Style
	{
		// Token: 0x06005E73 RID: 24179 RVA: 0x00180053 File Offset: 0x0017E253
		internal Style(ConstructionPhase phase)
		{
			if (phase == ConstructionPhase.Publishing)
			{
				this.m_styleAttributes = new StyleAttributeHashtable();
			}
		}

		// Token: 0x17002129 RID: 8489
		// (get) Token: 0x06005E74 RID: 24180 RVA: 0x00180070 File Offset: 0x0017E270
		// (set) Token: 0x06005E75 RID: 24181 RVA: 0x00180078 File Offset: 0x0017E278
		internal StyleAttributeHashtable StyleAttributes
		{
			get
			{
				return this.m_styleAttributes;
			}
			set
			{
				this.m_styleAttributes = value;
			}
		}

		// Token: 0x1700212A RID: 8490
		// (get) Token: 0x06005E76 RID: 24182 RVA: 0x00180081 File Offset: 0x0017E281
		// (set) Token: 0x06005E77 RID: 24183 RVA: 0x00180089 File Offset: 0x0017E289
		internal ExpressionInfoList ExpressionList
		{
			get
			{
				return this.m_expressionList;
			}
			set
			{
				this.m_expressionList = value;
			}
		}

		// Token: 0x1700212B RID: 8491
		// (get) Token: 0x06005E78 RID: 24184 RVA: 0x00180092 File Offset: 0x0017E292
		internal StyleExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x1700212C RID: 8492
		// (get) Token: 0x06005E79 RID: 24185 RVA: 0x0018009A File Offset: 0x0017E29A
		// (set) Token: 0x06005E7A RID: 24186 RVA: 0x001800A2 File Offset: 0x0017E2A2
		internal int CustomSharedStyleCount
		{
			get
			{
				return this.m_customSharedStyleCount;
			}
			set
			{
				this.m_customSharedStyleCount = value;
			}
		}

		// Token: 0x06005E7B RID: 24187 RVA: 0x001800AC File Offset: 0x0017E2AC
		internal void AddAttribute(string name, ExpressionInfo expressionInfo)
		{
			AttributeInfo attributeInfo = new AttributeInfo();
			attributeInfo.IsExpression = ExpressionInfo.Types.Constant != expressionInfo.Type;
			if (attributeInfo.IsExpression)
			{
				if (this.m_expressionList == null)
				{
					this.m_expressionList = new ExpressionInfoList();
				}
				attributeInfo.IntValue = this.m_expressionList.Add(expressionInfo);
			}
			else
			{
				attributeInfo.Value = expressionInfo.Value;
				attributeInfo.BoolValue = expressionInfo.BoolValue;
				attributeInfo.IntValue = expressionInfo.IntValue;
			}
			Global.Tracer.Assert(this.m_styleAttributes != null);
			this.m_styleAttributes.Add(name, attributeInfo);
		}

		// Token: 0x06005E7C RID: 24188 RVA: 0x00180144 File Offset: 0x0017E344
		internal void Initialize(InitializationContext context)
		{
			Global.Tracer.Assert(this.m_styleAttributes != null);
			IDictionaryEnumerator enumerator = this.m_styleAttributes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string text = (string)enumerator.Key;
				AttributeInfo attributeInfo = (AttributeInfo)enumerator.Value;
				Global.Tracer.Assert(text != null);
				Global.Tracer.Assert(attributeInfo != null);
				if (attributeInfo.IsExpression)
				{
					string text2 = text;
					if (text != null)
					{
						switch (text.Length)
						{
						case 14:
						{
							char c = text[6];
							if (c != 'C')
							{
								if (c != 'S')
								{
									if (c != 'W')
									{
										goto IL_0216;
									}
									if (!(text == "BorderWidthTop"))
									{
										goto IL_0216;
									}
									goto IL_0210;
								}
								else
								{
									if (!(text == "BorderStyleTop"))
									{
										goto IL_0216;
									}
									goto IL_0208;
								}
							}
							else if (!(text == "BorderColorTop"))
							{
								goto IL_0216;
							}
							break;
						}
						case 15:
						{
							char c = text[6];
							if (c != 'C')
							{
								if (c != 'S')
								{
									if (c != 'W')
									{
										goto IL_0216;
									}
									if (!(text == "BorderWidthLeft"))
									{
										goto IL_0216;
									}
									goto IL_0210;
								}
								else
								{
									if (!(text == "BorderStyleLeft"))
									{
										goto IL_0216;
									}
									goto IL_0208;
								}
							}
							else if (!(text == "BorderColorLeft"))
							{
								goto IL_0216;
							}
							break;
						}
						case 16:
						{
							char c = text[6];
							if (c != 'C')
							{
								if (c != 'S')
								{
									if (c != 'W')
									{
										goto IL_0216;
									}
									if (!(text == "BorderWidthRight"))
									{
										goto IL_0216;
									}
									goto IL_0210;
								}
								else
								{
									if (!(text == "BorderStyleRight"))
									{
										goto IL_0216;
									}
									goto IL_0208;
								}
							}
							else if (!(text == "BorderColorRight"))
							{
								goto IL_0216;
							}
							break;
						}
						case 17:
						{
							char c = text[6];
							if (c != 'C')
							{
								if (c != 'S')
								{
									if (c != 'W')
									{
										goto IL_0216;
									}
									if (!(text == "BorderWidthBottom"))
									{
										goto IL_0216;
									}
									goto IL_0210;
								}
								else
								{
									if (!(text == "BorderStyleBottom"))
									{
										goto IL_0216;
									}
									goto IL_0208;
								}
							}
							else if (!(text == "BorderColorBottom"))
							{
								goto IL_0216;
							}
							break;
						}
						default:
							goto IL_0216;
						}
						text = "BorderColor";
						goto IL_0216;
						IL_0208:
						text = "BorderStyle";
						goto IL_0216;
						IL_0210:
						text = "BorderWidth";
					}
					IL_0216:
					Global.Tracer.Assert(this.m_expressionList != null);
					ExpressionInfo expressionInfo = this.m_expressionList[attributeInfo.IntValue];
					expressionInfo.Initialize(text, context);
					context.ExprHostBuilder.StyleAttribute(text2, expressionInfo);
				}
			}
			AttributeInfo attributeInfo2 = this.m_styleAttributes["BackgroundImageSource"];
			if (attributeInfo2 != null)
			{
				Global.Tracer.Assert(!attributeInfo2.IsExpression);
				Image.SourceType intValue = (Image.SourceType)attributeInfo2.IntValue;
				if (Image.SourceType.Embedded == intValue)
				{
					AttributeInfo attributeInfo3 = this.m_styleAttributes["BackgroundImageValue"];
					Global.Tracer.Assert(attributeInfo3 != null);
					PublishingValidator.ValidateEmbeddedImageName(attributeInfo3, context.EmbeddedImages, context.ObjectType, context.ObjectName, "BackgroundImageValue", context.ErrorContext);
				}
				else if (intValue == Image.SourceType.External)
				{
					AttributeInfo attributeInfo4 = this.m_styleAttributes["BackgroundImageValue"];
					Global.Tracer.Assert(attributeInfo4 != null);
					if (!attributeInfo4.IsExpression)
					{
						context.ImageStreamNames[attributeInfo4.Value] = new ImageInfo(context.ObjectName, null);
					}
				}
			}
			context.CheckInternationalSettings(this.m_styleAttributes);
		}

		// Token: 0x06005E7D RID: 24189 RVA: 0x0018048F File Offset: 0x0017E68F
		internal void SetStyleExprHost(StyleExprHost exprHost)
		{
			Global.Tracer.Assert(exprHost != null);
			this.m_exprHost = exprHost;
		}

		// Token: 0x06005E7E RID: 24190 RVA: 0x001804A8 File Offset: 0x0017E6A8
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.StyleAttributes, ObjectType.StyleAttributeHashtable),
				new MemberInfo(MemberName.ExpressionList, ObjectType.ExpressionInfoList)
			});
		}

		// Token: 0x04003033 RID: 12339
		private StyleAttributeHashtable m_styleAttributes;

		// Token: 0x04003034 RID: 12340
		private ExpressionInfoList m_expressionList;

		// Token: 0x04003035 RID: 12341
		[NonSerialized]
		private StyleExprHost m_exprHost;

		// Token: 0x04003036 RID: 12342
		[NonSerialized]
		private int m_customSharedStyleCount = -1;
	}
}
