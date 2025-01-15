using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002124 RID: 8484
	internal abstract class OpenXmlReader : IDisposable
	{
		// Token: 0x0600D1F3 RID: 53747 RVA: 0x000020FD File Offset: 0x000002FD
		protected OpenXmlReader()
		{
		}

		// Token: 0x0600D1F4 RID: 53748 RVA: 0x0029C3D0 File Offset: 0x0029A5D0
		protected OpenXmlReader(bool readMiscNodes)
		{
			this._readMiscNodes = readMiscNodes;
		}

		// Token: 0x0600D1F5 RID: 53749 RVA: 0x0029C3DF File Offset: 0x0029A5DF
		public static OpenXmlReader Create(OpenXmlPart openXmlPart)
		{
			return new OpenXmlPartReader(openXmlPart);
		}

		// Token: 0x0600D1F6 RID: 53750 RVA: 0x0029C3E7 File Offset: 0x0029A5E7
		public static OpenXmlReader Create(OpenXmlPart openXmlPart, bool readMiscNodes)
		{
			return new OpenXmlPartReader(openXmlPart, readMiscNodes);
		}

		// Token: 0x0600D1F7 RID: 53751 RVA: 0x0029C3F0 File Offset: 0x0029A5F0
		public static OpenXmlReader Create(Stream partStream)
		{
			return new OpenXmlPartReader(partStream);
		}

		// Token: 0x0600D1F8 RID: 53752 RVA: 0x0029C3F8 File Offset: 0x0029A5F8
		public static OpenXmlReader Create(Stream partStream, bool readMiscNodes)
		{
			return new OpenXmlPartReader(partStream, readMiscNodes);
		}

		// Token: 0x0600D1F9 RID: 53753 RVA: 0x0029C401 File Offset: 0x0029A601
		public static OpenXmlReader Create(OpenXmlElement openXmlElement)
		{
			return new OpenXmlDomReader(openXmlElement);
		}

		// Token: 0x0600D1FA RID: 53754 RVA: 0x0029C409 File Offset: 0x0029A609
		public static OpenXmlReader Create(OpenXmlElement openXmlElement, bool readMiscNodes)
		{
			return new OpenXmlDomReader(openXmlElement, readMiscNodes);
		}

		// Token: 0x170032C1 RID: 12993
		// (get) Token: 0x0600D1FB RID: 53755 RVA: 0x0029C412 File Offset: 0x0029A612
		public bool ReadMiscNodes
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._readMiscNodes;
			}
		}

		// Token: 0x170032C2 RID: 12994
		// (get) Token: 0x0600D1FC RID: 53756 RVA: 0x0029C420 File Offset: 0x0029A620
		public virtual string Encoding
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return null;
			}
		}

		// Token: 0x170032C3 RID: 12995
		// (get) Token: 0x0600D1FD RID: 53757 RVA: 0x0029C42C File Offset: 0x0029A62C
		public virtual bool? StandaloneXml
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return null;
			}
		}

		// Token: 0x170032C4 RID: 12996
		// (get) Token: 0x0600D1FE RID: 53758 RVA: 0x0029C448 File Offset: 0x0029A648
		public virtual bool HasAttributes
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this.Attributes.Count > 0;
			}
		}

		// Token: 0x170032C5 RID: 12997
		// (get) Token: 0x0600D1FF RID: 53759
		public abstract ReadOnlyCollection<OpenXmlAttribute> Attributes { get; }

		// Token: 0x170032C6 RID: 12998
		// (get) Token: 0x0600D200 RID: 53760
		public abstract IEnumerable<KeyValuePair<string, string>> NamespaceDeclarations { get; }

		// Token: 0x170032C7 RID: 12999
		// (get) Token: 0x0600D201 RID: 53761
		public abstract Type ElementType { get; }

		// Token: 0x170032C8 RID: 13000
		// (get) Token: 0x0600D202 RID: 53762
		public abstract bool IsMiscNode { get; }

		// Token: 0x170032C9 RID: 13001
		// (get) Token: 0x0600D203 RID: 53763
		public abstract bool IsStartElement { get; }

		// Token: 0x170032CA RID: 13002
		// (get) Token: 0x0600D204 RID: 53764
		public abstract bool IsEndElement { get; }

		// Token: 0x170032CB RID: 13003
		// (get) Token: 0x0600D205 RID: 53765
		public abstract int Depth { get; }

		// Token: 0x170032CC RID: 13004
		// (get) Token: 0x0600D206 RID: 53766
		public abstract bool EOF { get; }

		// Token: 0x170032CD RID: 13005
		// (get) Token: 0x0600D207 RID: 53767
		public abstract string LocalName { get; }

		// Token: 0x170032CE RID: 13006
		// (get) Token: 0x0600D208 RID: 53768
		public abstract string NamespaceUri { get; }

		// Token: 0x170032CF RID: 13007
		// (get) Token: 0x0600D209 RID: 53769
		public abstract string Prefix { get; }

		// Token: 0x0600D20A RID: 53770
		public abstract bool Read();

		// Token: 0x0600D20B RID: 53771
		public abstract bool ReadFirstChild();

		// Token: 0x0600D20C RID: 53772
		public abstract bool ReadNextSibling();

		// Token: 0x0600D20D RID: 53773
		public abstract void Skip();

		// Token: 0x0600D20E RID: 53774
		public abstract OpenXmlElement LoadCurrentElement();

		// Token: 0x0600D20F RID: 53775
		public abstract string GetText();

		// Token: 0x0600D210 RID: 53776
		public abstract void Close();

		// Token: 0x0600D211 RID: 53777 RVA: 0x0029C45E File Offset: 0x0029A65E
		protected virtual void ThrowIfObjectDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x0600D212 RID: 53778 RVA: 0x0029C479 File Offset: 0x0029A679
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					this.Close();
				}
				this._disposed = true;
			}
		}

		// Token: 0x0600D213 RID: 53779 RVA: 0x0029C493 File Offset: 0x0029A693
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400696B RID: 26987
		private bool _disposed;

		// Token: 0x0400696C RID: 26988
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool _readMiscNodes;
	}
}
