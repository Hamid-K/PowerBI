using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Validation
{
	// Token: 0x0200314F RID: 12623
	internal static class ValidationTraverser
	{
		// Token: 0x0601B5DF RID: 112095 RVA: 0x00376D70 File Offset: 0x00374F70
		internal static void ValidatingTraverse(ValidationContext validationContext, ValidationTraverser.ValidationAction validateAction, ValidationTraverser.ValidationAction finishAction, ValidationTraverser.GetStopSignal getStopSignal)
		{
			if (getStopSignal != null && getStopSignal(validationContext))
			{
				return;
			}
			OpenXmlElement element = validationContext.Element;
			bool flag = false;
			validationContext.McContext.PushMCAttributes2(element.MCAttributes, (string prefix) => element.LookupNamespace(prefix));
			if (element.IsStrongTypedElement())
			{
				if (element.IsInVersion(validationContext.FileFormat))
				{
					validateAction(validationContext);
					flag = true;
				}
				else
				{
					validationContext.McContext.IsProcessContent(element);
				}
				using (IEnumerator<OpenXmlElement> enumerator = element.ChildElements.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						OpenXmlElement openXmlElement = enumerator.Current;
						validationContext.Element = openXmlElement;
						ValidationTraverser.ValidatingTraverse(validationContext, validateAction, finishAction, getStopSignal);
					}
					goto IL_01E2;
				}
			}
			if (element.ElementTypeId == 9002 || element.ElementTypeId == 9002)
			{
				if (!validationContext.McContext.IsProcessContent(element))
				{
					goto IL_01E2;
				}
				using (IEnumerator<OpenXmlElement> enumerator2 = element.ChildElements.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						OpenXmlElement openXmlElement2 = enumerator2.Current;
						validationContext.Element = openXmlElement2;
						ValidationTraverser.ValidatingTraverse(validationContext, validateAction, finishAction, getStopSignal);
					}
					goto IL_01E2;
				}
			}
			if (element.ElementTypeId == 9003)
			{
				validateAction(validationContext);
				flag = true;
				OpenXmlCompositeElement contentFromACBlock = validationContext.McContext.GetContentFromACBlock((AlternateContent)element, validationContext.FileFormat);
				if (contentFromACBlock == null)
				{
					goto IL_01E2;
				}
				using (IEnumerator<OpenXmlElement> enumerator3 = contentFromACBlock.ChildElements.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						OpenXmlElement openXmlElement3 = enumerator3.Current;
						validationContext.Element = openXmlElement3;
						ValidationTraverser.ValidatingTraverse(validationContext, validateAction, finishAction, getStopSignal);
					}
					goto IL_01E2;
				}
			}
			int elementTypeId = element.ElementTypeId;
			IL_01E2:
			validationContext.McContext.PopMCAttributes2();
			if (flag && finishAction != null)
			{
				validationContext.Element = element;
				finishAction(validationContext);
			}
		}

		// Token: 0x02003150 RID: 12624
		// (Invoke) Token: 0x0601B5E1 RID: 112097
		internal delegate void ValidationAction(ValidationContext validationContext);

		// Token: 0x02003151 RID: 12625
		// (Invoke) Token: 0x0601B5E5 RID: 112101
		internal delegate bool GetStopSignal(ValidationContext validationContext);
	}
}
