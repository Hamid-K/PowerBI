using System;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200210D RID: 8461
	public class OpenXmlElementContext
	{
		// Token: 0x1700327A RID: 12922
		// (get) Token: 0x0600D0F2 RID: 53490 RVA: 0x00299BC0 File Offset: 0x00297DC0
		// (set) Token: 0x0600D0F3 RID: 53491 RVA: 0x00299BDD File Offset: 0x00297DDD
		internal MarkupCompatibilityProcessSettings MCSettings
		{
			get
			{
				if (this._mcSettings == null)
				{
					this._mcSettings = new MarkupCompatibilityProcessSettings(MarkupCompatibilityProcessMode.NoProcess, FileFormatVersions.Office2007 | FileFormatVersions.Office2010);
				}
				return this._mcSettings;
			}
			set
			{
				this._mcSettings = value;
			}
		}

		// Token: 0x1700327B RID: 12923
		// (get) Token: 0x0600D0F4 RID: 53492 RVA: 0x00299BE6 File Offset: 0x00297DE6
		// (set) Token: 0x0600D0F5 RID: 53493 RVA: 0x00299BEE File Offset: 0x00297DEE
		internal OpenXmlPart OwnerPart
		{
			get
			{
				return this._ownerPart;
			}
			set
			{
				this._ownerPart = value;
			}
		}

		// Token: 0x1700327C RID: 12924
		// (get) Token: 0x0600D0F6 RID: 53494 RVA: 0x00299BF7 File Offset: 0x00297DF7
		private XmlNameTable XmlNameTable
		{
			get
			{
				return this._xmlNameTable;
			}
		}

		// Token: 0x1700327D RID: 12925
		// (get) Token: 0x0600D0F7 RID: 53495 RVA: 0x00299BFF File Offset: 0x00297DFF
		// (set) Token: 0x0600D0F8 RID: 53496 RVA: 0x00299C07 File Offset: 0x00297E07
		internal XmlReaderSettings XmlReaderSettings
		{
			get
			{
				return this._xmlReaderSettings;
			}
			set
			{
				this._xmlReaderSettings = value;
			}
		}

		// Token: 0x1700327E RID: 12926
		// (get) Token: 0x0600D0F9 RID: 53497 RVA: 0x00299C10 File Offset: 0x00297E10
		// (set) Token: 0x0600D0FA RID: 53498 RVA: 0x00299C18 File Offset: 0x00297E18
		internal OpenXmlLoadMode LoadMode
		{
			get
			{
				return this._loadMode;
			}
			set
			{
				this._loadMode = value;
			}
		}

		// Token: 0x1700327F RID: 12927
		// (get) Token: 0x0600D0FB RID: 53499 RVA: 0x00299C21 File Offset: 0x00297E21
		// (set) Token: 0x0600D0FC RID: 53500 RVA: 0x00299C29 File Offset: 0x00297E29
		internal int LazySteps
		{
			get
			{
				return this._lazySteps;
			}
			set
			{
				if (value < 0)
				{
					this._lazySteps = 0;
					return;
				}
				this._lazySteps = value;
			}
		}

		// Token: 0x0600D0FD RID: 53501 RVA: 0x00299C3E File Offset: 0x00297E3E
		public OpenXmlElementContext()
		{
			this._xmlNameTable = new NameTable();
			this.MCContext = new MCContext();
			this.Init();
		}

		// Token: 0x0600D0FE RID: 53502 RVA: 0x00299C6C File Offset: 0x00297E6C
		internal static XmlReaderSettings CreateDefaultXmlReaderSettings()
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.NameTable = new NameTable();
			xmlReaderSettings.IgnoreWhitespace = true;
			for (int i = 1; i < NamespaceIdMap.Count; i++)
			{
				xmlReaderSettings.NameTable.Add(NamespaceIdMap.GetNamespaceUri((byte)i));
			}
			xmlReaderSettings.NameTable.Add("http://www.w3.org/2000/xmlns/");
			return xmlReaderSettings;
		}

		// Token: 0x0600D0FF RID: 53503 RVA: 0x00299CCC File Offset: 0x00297ECC
		internal bool IsXmlnsUri(string nsUri)
		{
			return this._xmlNameTable.Get(nsUri) == "http://www.w3.org/2000/xmlns/";
		}

		// Token: 0x0600D100 RID: 53504 RVA: 0x00299CE4 File Offset: 0x00297EE4
		private void Init()
		{
			for (int i = 1; i < NamespaceIdMap.Count; i++)
			{
				this._xmlNameTable.Add(NamespaceIdMap.GetNamespaceUri((byte)i));
			}
			this._xmlNameTable.Add("http://www.w3.org/2000/xmlns/");
			this.XmlReaderSettings = new XmlReaderSettings();
			this.XmlReaderSettings.NameTable = this.XmlNameTable;
			this.XmlReaderSettings.IgnoreWhitespace = true;
		}

		// Token: 0x0600D101 RID: 53505 RVA: 0x00299D4D File Offset: 0x00297F4D
		internal void ElementInsertingEvent(OpenXmlElement element, OpenXmlElement parent)
		{
			if (this._onElementInserting != null)
			{
				this._onElementInserting(this, new ElementEventArgs(element, parent));
			}
		}

		// Token: 0x0600D102 RID: 53506 RVA: 0x00299D6A File Offset: 0x00297F6A
		internal void ElementInsertedEvent(OpenXmlElement element, OpenXmlElement parent)
		{
			if (this._onElementInserted != null)
			{
				this._onElementInserted(this, new ElementEventArgs(element, parent));
			}
		}

		// Token: 0x0600D103 RID: 53507 RVA: 0x00299D87 File Offset: 0x00297F87
		internal void ElementRemovingEvent(OpenXmlElement element, OpenXmlElement parent)
		{
			if (this._onElementRemoving != null)
			{
				this._onElementRemoving(this, new ElementEventArgs(element, parent));
			}
		}

		// Token: 0x0600D104 RID: 53508 RVA: 0x00299DA4 File Offset: 0x00297FA4
		internal void ElementRemovedEvent(OpenXmlElement element, OpenXmlElement parent)
		{
			if (this._onElementRemoved != null)
			{
				this._onElementRemoved(this, new ElementEventArgs(element, parent));
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600D105 RID: 53509 RVA: 0x00299DC1 File Offset: 0x00297FC1
		// (remove) Token: 0x0600D106 RID: 53510 RVA: 0x00299DDA File Offset: 0x00297FDA
		public event EventHandler<ElementEventArgs> ElementInserting
		{
			add
			{
				this._onElementInserting = (EventHandler<ElementEventArgs>)Delegate.Combine(this._onElementInserting, value);
			}
			remove
			{
				this._onElementInserting = (EventHandler<ElementEventArgs>)Delegate.Remove(this._onElementInserting, value);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600D107 RID: 53511 RVA: 0x00299DF3 File Offset: 0x00297FF3
		// (remove) Token: 0x0600D108 RID: 53512 RVA: 0x00299E0C File Offset: 0x0029800C
		public event EventHandler<ElementEventArgs> ElementInserted
		{
			add
			{
				this._onElementInserted = (EventHandler<ElementEventArgs>)Delegate.Combine(this._onElementInserted, value);
			}
			remove
			{
				this._onElementInserted = (EventHandler<ElementEventArgs>)Delegate.Remove(this._onElementInserted, value);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600D109 RID: 53513 RVA: 0x00299E25 File Offset: 0x00298025
		// (remove) Token: 0x0600D10A RID: 53514 RVA: 0x00299E3E File Offset: 0x0029803E
		public event EventHandler<ElementEventArgs> ElementRemoving
		{
			add
			{
				this._onElementRemoving = (EventHandler<ElementEventArgs>)Delegate.Combine(this._onElementRemoving, value);
			}
			remove
			{
				this._onElementRemoving = (EventHandler<ElementEventArgs>)Delegate.Remove(this._onElementRemoving, value);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600D10B RID: 53515 RVA: 0x00299E57 File Offset: 0x00298057
		// (remove) Token: 0x0600D10C RID: 53516 RVA: 0x00299E70 File Offset: 0x00298070
		public event EventHandler<ElementEventArgs> ElementRemoved
		{
			add
			{
				this._onElementRemoved = (EventHandler<ElementEventArgs>)Delegate.Combine(this._onElementRemoved, value);
			}
			remove
			{
				this._onElementRemoved = (EventHandler<ElementEventArgs>)Delegate.Remove(this._onElementRemoved, value);
			}
		}

		// Token: 0x17003280 RID: 12928
		// (get) Token: 0x0600D10D RID: 53517 RVA: 0x00299E89 File Offset: 0x00298089
		// (set) Token: 0x0600D10E RID: 53518 RVA: 0x00299E91 File Offset: 0x00298091
		internal MCContext MCContext { get; set; }

		// Token: 0x17003281 RID: 12929
		// (get) Token: 0x0600D10F RID: 53519 RVA: 0x00299E9A File Offset: 0x0029809A
		// (set) Token: 0x0600D110 RID: 53520 RVA: 0x00299EA2 File Offset: 0x002980A2
		internal uint ACBlockLevel { get; set; }

		// Token: 0x04006921 RID: 26913
		internal const string xmlnsUri = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04006922 RID: 26914
		internal const string xmlnsPrefix = "xmlns";

		// Token: 0x04006923 RID: 26915
		private OpenXmlPart _ownerPart;

		// Token: 0x04006924 RID: 26916
		private XmlNameTable _xmlNameTable;

		// Token: 0x04006925 RID: 26917
		private XmlReaderSettings _xmlReaderSettings;

		// Token: 0x04006926 RID: 26918
		private OpenXmlLoadMode _loadMode;

		// Token: 0x04006927 RID: 26919
		private int _lazySteps = 3;

		// Token: 0x04006928 RID: 26920
		private EventHandler<ElementEventArgs> _onElementInserting;

		// Token: 0x04006929 RID: 26921
		private EventHandler<ElementEventArgs> _onElementInserted;

		// Token: 0x0400692A RID: 26922
		private EventHandler<ElementEventArgs> _onElementRemoving;

		// Token: 0x0400692B RID: 26923
		private EventHandler<ElementEventArgs> _onElementRemoved;

		// Token: 0x0400692C RID: 26924
		private MarkupCompatibilityProcessSettings _mcSettings;
	}
}
