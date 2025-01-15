using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020020EB RID: 8427
	internal class MCContext
	{
		// Token: 0x0600CF46 RID: 53062 RVA: 0x002941C0 File Offset: 0x002923C0
		internal MCContext()
		{
			this._currentIgnorable = new Stack<string>();
			this._currentPreserveAttr = new Stack<XmlQualifiedName>();
			this._currentPreserveEle = new Stack<XmlQualifiedName>();
			this._currentProcessContent = new Stack<XmlQualifiedName>();
			this._pushedIgnor = new Stack<int>();
			this._pushedPA = new Stack<int>();
			this._pushedPC = new Stack<int>();
			this._pushedPE = new Stack<int>();
		}

		// Token: 0x0600CF47 RID: 53063 RVA: 0x0029422B File Offset: 0x0029242B
		internal MCContext(bool exceptionOnError)
			: this()
		{
			this._noExceptionOnError = !exceptionOnError;
		}

		// Token: 0x170031D5 RID: 12757
		// (get) Token: 0x0600CF48 RID: 53064 RVA: 0x0029423D File Offset: 0x0029243D
		// (set) Token: 0x0600CF49 RID: 53065 RVA: 0x00294245 File Offset: 0x00292445
		internal MCContext.LookupNamespace LookupNamespaceDelegate { get; set; }

		// Token: 0x0600CF4A RID: 53066 RVA: 0x00294250 File Offset: 0x00292450
		internal void PushMCAttributes(MarkupCompatibilityAttributes attr)
		{
			this._pushedIgnor.Push(this.PushIgnorable(attr));
			this._pushedPA.Push(this.PushPreserveAttribute(attr));
			this._pushedPE.Push(this.PushPreserveElement(attr));
			this._pushedPC.Push(this.PushProcessContent(attr));
		}

		// Token: 0x0600CF4B RID: 53067 RVA: 0x002942A8 File Offset: 0x002924A8
		internal void PopMCAttributes()
		{
			this.PopIgnorable(this._pushedIgnor.Pop());
			this.PopPreserveAttribute(this._pushedPA.Pop());
			this.PopPreserveElement(this._pushedPE.Pop());
			this.PopProcessContent(this._pushedPC.Pop());
		}

		// Token: 0x0600CF4C RID: 53068 RVA: 0x002942F9 File Offset: 0x002924F9
		internal void PushMCAttributes2(MarkupCompatibilityAttributes attr, MCContext.LookupNamespace lookupNamespaceDelegate)
		{
			this.LookupNamespaceDelegate = lookupNamespaceDelegate;
			this._pushedIgnor.Push(this.PushIgnorable(attr));
			this._pushedPC.Push(this.PushProcessContent(attr));
		}

		// Token: 0x0600CF4D RID: 53069 RVA: 0x00294326 File Offset: 0x00292526
		internal void PopMCAttributes2()
		{
			this.PopIgnorable(this._pushedIgnor.Pop());
			this.PopProcessContent(this._pushedPC.Pop());
		}

		// Token: 0x0600CF4E RID: 53070 RVA: 0x0029434C File Offset: 0x0029254C
		internal IEnumerable<string> ParsePrefixList(string ignorable, MCContext.OnInvalidValue onInvalidPrefix)
		{
			string[] prefixes = ignorable.Trim().Split(new char[] { ' ' });
			foreach (string prefix in prefixes)
			{
				string ns = this.LookupNamespaceDelegate(prefix);
				if (string.IsNullOrEmpty(ns))
				{
					if (onInvalidPrefix(ignorable))
					{
						yield break;
					}
				}
				else
				{
					yield return ns;
				}
			}
			yield break;
		}

		// Token: 0x0600CF4F RID: 53071 RVA: 0x00294378 File Offset: 0x00292578
		internal IEnumerable<XmlQualifiedName> ParseQNameList(string qnameList, MCContext.OnInvalidValue onInvalidQName)
		{
			string[] qnames = qnameList.Trim().Split(new char[] { ' ' });
			foreach (string qname in qnames)
			{
				string[] items = qname.Split(new char[] { ':' });
				if (items.Length != 2)
				{
					if (onInvalidQName(qnameList))
					{
						yield break;
					}
				}
				else
				{
					string ns = this.LookupNamespaceDelegate(items[0]);
					if (string.IsNullOrEmpty(ns))
					{
						if (onInvalidQName(qnameList))
						{
							yield break;
						}
					}
					else
					{
						yield return new XmlQualifiedName(items[1], ns);
					}
				}
			}
			yield break;
		}

		// Token: 0x0600CF50 RID: 53072 RVA: 0x002943A3 File Offset: 0x002925A3
		internal bool HasIgnorable()
		{
			return this._currentIgnorable.Count > 0;
		}

		// Token: 0x0600CF51 RID: 53073 RVA: 0x002943B3 File Offset: 0x002925B3
		internal bool IsIgnorableNs(byte namespaceId)
		{
			return this._currentIgnorable.Count != 0 && this._currentIgnorable.Contains(NamespaceIdMap.GetNamespaceUri(namespaceId));
		}

		// Token: 0x0600CF52 RID: 53074 RVA: 0x002943DA File Offset: 0x002925DA
		internal bool IsIgnorableNs(string ns)
		{
			return this._currentIgnorable.Count != 0 && !string.IsNullOrEmpty(ns) && this._currentIgnorable.Contains(ns);
		}

		// Token: 0x0600CF53 RID: 53075 RVA: 0x00294406 File Offset: 0x00292606
		internal bool IsPreservedAttribute(string ns, string localName)
		{
			return MCContext.ContainsQName(localName, ns, this._currentPreserveAttr);
		}

		// Token: 0x0600CF54 RID: 53076 RVA: 0x00294415 File Offset: 0x00292615
		internal bool IsPreservedElement(string ns, string localName)
		{
			return MCContext.ContainsQName(localName, ns, this._currentPreserveEle);
		}

		// Token: 0x0600CF55 RID: 53077 RVA: 0x00294424 File Offset: 0x00292624
		internal bool IsProcessContent(string ns, string localName)
		{
			return MCContext.ContainsQName(localName, ns, this._currentProcessContent);
		}

		// Token: 0x0600CF56 RID: 53078 RVA: 0x00294433 File Offset: 0x00292633
		internal bool IsProcessContent(OpenXmlElement element)
		{
			return MCContext.ContainsQName(element.LocalName, element.NamespaceUri, this._currentProcessContent);
		}

		// Token: 0x0600CF57 RID: 53079 RVA: 0x0029444C File Offset: 0x0029264C
		internal AttributeAction GetAttributeAction(string ns, string localName, FileFormatVersions format)
		{
			if (format == (FileFormatVersions.Office2007 | FileFormatVersions.Office2010))
			{
				return AttributeAction.Normal;
			}
			if (string.IsNullOrEmpty(ns))
			{
				return AttributeAction.Normal;
			}
			if (NamespaceIdMap.IsInFileFormat(ns, format))
			{
				return AttributeAction.Normal;
			}
			if (!this.IsIgnorableNs(ns))
			{
				return AttributeAction.Normal;
			}
			if (this.IsPreservedAttribute(ns, localName))
			{
				return AttributeAction.Normal;
			}
			return AttributeAction.Ignore;
		}

		// Token: 0x0600CF58 RID: 53080 RVA: 0x00294484 File Offset: 0x00292684
		internal ElementAction GetElementAction(OpenXmlElement element, FileFormatVersions format)
		{
			if (format == (FileFormatVersions.Office2007 | FileFormatVersions.Office2010))
			{
				return ElementAction.Normal;
			}
			if (element is AlternateContent)
			{
				return ElementAction.ACBlock;
			}
			if (element.IsInVersion(format))
			{
				return ElementAction.Normal;
			}
			if (!this.IsIgnorableNs(element.NamespaceUri))
			{
				return ElementAction.Normal;
			}
			if (this.IsPreservedElement(element.NamespaceUri, element.LocalName))
			{
				return ElementAction.Normal;
			}
			if (this.IsProcessContent(element.NamespaceUri, element.LocalName))
			{
				return ElementAction.ProcessContent;
			}
			return ElementAction.Ignore;
		}

		// Token: 0x0600CF59 RID: 53081 RVA: 0x002944EC File Offset: 0x002926EC
		private static bool ContainsQName(string localName, string ns, Stack<XmlQualifiedName> stack)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(localName, ns);
			foreach (XmlQualifiedName xmlQualifiedName2 in stack)
			{
				if (xmlQualifiedName2 == xmlQualifiedName || (xmlQualifiedName2.Name == "*" && xmlQualifiedName2.Namespace == ns))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600CF5A RID: 53082 RVA: 0x0029456C File Offset: 0x0029276C
		private bool OnMcContextError(string value)
		{
			if (this._noExceptionOnError)
			{
				return false;
			}
			string text = string.Format(CultureInfo.CurrentUICulture, ExceptionMessages.UnknowMCContent, new object[] { value });
			throw new InvalidMCContentException(text);
		}

		// Token: 0x0600CF5B RID: 53083 RVA: 0x002945A8 File Offset: 0x002927A8
		private int PushIgnorable(MarkupCompatibilityAttributes attr)
		{
			int num = 0;
			if (attr != null && attr.Ignorable != null && !string.IsNullOrEmpty(attr.Ignorable.Value))
			{
				foreach (string text in this.ParsePrefixList(attr.Ignorable, new MCContext.OnInvalidValue(this.OnMcContextError)))
				{
					this._currentIgnorable.Push(text);
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600CF5C RID: 53084 RVA: 0x00294638 File Offset: 0x00292838
		private int PushQName(Stack<XmlQualifiedName> stack, string value)
		{
			int num = 0;
			foreach (XmlQualifiedName xmlQualifiedName in this.ParseQNameList(value, new MCContext.OnInvalidValue(this.OnMcContextError)))
			{
				stack.Push(xmlQualifiedName);
				num++;
			}
			return num;
		}

		// Token: 0x0600CF5D RID: 53085 RVA: 0x0029469C File Offset: 0x0029289C
		private int PushPreserveAttribute(MarkupCompatibilityAttributes attr)
		{
			int num = 0;
			if (attr != null && attr.PreserveAttributes != null && !string.IsNullOrEmpty(attr.PreserveAttributes.Value))
			{
				num = this.PushQName(this._currentPreserveAttr, attr.PreserveAttributes.Value);
			}
			return num;
		}

		// Token: 0x0600CF5E RID: 53086 RVA: 0x002946E4 File Offset: 0x002928E4
		private int PushPreserveElement(MarkupCompatibilityAttributes attr)
		{
			int num = 0;
			if (attr != null && attr.PreserveElements != null && !string.IsNullOrEmpty(attr.PreserveElements.Value))
			{
				num = this.PushQName(this._currentPreserveEle, attr.PreserveElements.Value);
			}
			return num;
		}

		// Token: 0x0600CF5F RID: 53087 RVA: 0x0029472C File Offset: 0x0029292C
		private int PushProcessContent(MarkupCompatibilityAttributes attr)
		{
			int num = 0;
			if (attr != null && attr.ProcessContent != null && !string.IsNullOrEmpty(attr.ProcessContent.Value))
			{
				num = this.PushQName(this._currentProcessContent, attr.ProcessContent.Value);
			}
			return num;
		}

		// Token: 0x0600CF60 RID: 53088 RVA: 0x00294774 File Offset: 0x00292974
		private void PopIgnorable(int count)
		{
			for (int i = 0; i < count; i++)
			{
				this._currentIgnorable.Pop();
			}
		}

		// Token: 0x0600CF61 RID: 53089 RVA: 0x0029479C File Offset: 0x0029299C
		private void PopPreserveAttribute(int count)
		{
			for (int i = 0; i < count; i++)
			{
				this._currentPreserveAttr.Pop();
			}
		}

		// Token: 0x0600CF62 RID: 53090 RVA: 0x002947C4 File Offset: 0x002929C4
		private void PopPreserveElement(int count)
		{
			for (int i = 0; i < count; i++)
			{
				this._currentPreserveEle.Pop();
			}
		}

		// Token: 0x0600CF63 RID: 53091 RVA: 0x002947EC File Offset: 0x002929EC
		private void PopProcessContent(int count)
		{
			for (int i = 0; i < count; i++)
			{
				this._currentProcessContent.Pop();
			}
		}

		// Token: 0x0600CF64 RID: 53092 RVA: 0x00294814 File Offset: 0x00292A14
		internal OpenXmlCompositeElement GetContentFromACBlock(AlternateContent acblk, FileFormatVersions format)
		{
			foreach (AlternateContentChoice alternateContentChoice in acblk.ChildElements.OfType<AlternateContentChoice>())
			{
				if (alternateContentChoice.Requires != null)
				{
					string text = alternateContentChoice.Requires.InnerText.Trim();
					if (!string.IsNullOrEmpty(text))
					{
						bool flag = true;
						string[] array = text.Split(new char[] { ' ' });
						int i = 0;
						while (i < array.Length)
						{
							string text2 = array[i];
							string text3 = alternateContentChoice.LookupNamespace(text2);
							if (text3 == null)
							{
								if (this._noExceptionOnError)
								{
									flag = false;
									break;
								}
								string text4 = string.Format(CultureInfo.CurrentUICulture, ExceptionMessages.UnknowMCContent, new object[] { text2 });
								throw new InvalidMCContentException(text4);
							}
							else
							{
								if (!NamespaceIdMap.IsInFileFormat(text3, format))
								{
									flag = false;
									break;
								}
								i++;
							}
						}
						if (flag)
						{
							return alternateContentChoice;
						}
					}
				}
			}
			AlternateContentFallback firstChild = acblk.GetFirstChild<AlternateContentFallback>();
			if (firstChild != null)
			{
				return firstChild;
			}
			return null;
		}

		// Token: 0x04006887 RID: 26759
		private Stack<string> _currentIgnorable;

		// Token: 0x04006888 RID: 26760
		private Stack<XmlQualifiedName> _currentPreserveAttr;

		// Token: 0x04006889 RID: 26761
		private Stack<XmlQualifiedName> _currentPreserveEle;

		// Token: 0x0400688A RID: 26762
		private Stack<XmlQualifiedName> _currentProcessContent;

		// Token: 0x0400688B RID: 26763
		private Stack<int> _pushedIgnor;

		// Token: 0x0400688C RID: 26764
		private Stack<int> _pushedPA;

		// Token: 0x0400688D RID: 26765
		private Stack<int> _pushedPE;

		// Token: 0x0400688E RID: 26766
		private Stack<int> _pushedPC;

		// Token: 0x0400688F RID: 26767
		private bool _noExceptionOnError;

		// Token: 0x020020EC RID: 8428
		// (Invoke) Token: 0x0600CF66 RID: 53094
		internal delegate string LookupNamespace(string prefix);

		// Token: 0x020020ED RID: 8429
		// (Invoke) Token: 0x0600CF6A RID: 53098
		internal delegate bool OnInvalidValue(string value);
	}
}
