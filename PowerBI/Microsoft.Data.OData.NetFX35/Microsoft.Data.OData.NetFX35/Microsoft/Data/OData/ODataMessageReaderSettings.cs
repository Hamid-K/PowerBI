using System;
using System.Xml;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x0200024F RID: 591
	public sealed class ODataMessageReaderSettings : ODataMessageReaderSettingsBase
	{
		// Token: 0x0600120A RID: 4618 RVA: 0x00044588 File Offset: 0x00042788
		public ODataMessageReaderSettings()
		{
			this.DisablePrimitiveTypeConversion = false;
			this.DisableMessageStreamDisposal = false;
			this.UndeclaredPropertyBehaviorKinds = ODataUndeclaredPropertyBehaviorKinds.None;
			this.readerBehavior = ODataReaderBehavior.DefaultBehavior;
			this.MaxProtocolVersion = ODataVersion.V3;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000445B8 File Offset: 0x000427B8
		public ODataMessageReaderSettings(ODataMessageReaderSettings other)
			: base(other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(other, "other");
			this.BaseUri = other.BaseUri;
			this.DisableMessageStreamDisposal = other.DisableMessageStreamDisposal;
			this.DisablePrimitiveTypeConversion = other.DisablePrimitiveTypeConversion;
			this.UndeclaredPropertyBehaviorKinds = other.UndeclaredPropertyBehaviorKinds;
			this.MaxProtocolVersion = other.MaxProtocolVersion;
			this.atomFormatEntryXmlCustomizationCallback = other.atomFormatEntryXmlCustomizationCallback;
			this.readerBehavior = other.ReaderBehavior;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0004462B File Offset: 0x0004282B
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x00044633 File Offset: 0x00042833
		public Uri BaseUri { get; set; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0004463C File Offset: 0x0004283C
		// (set) Token: 0x0600120F RID: 4623 RVA: 0x00044644 File Offset: 0x00042844
		public bool DisablePrimitiveTypeConversion { get; set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x0004464D File Offset: 0x0004284D
		// (set) Token: 0x06001211 RID: 4625 RVA: 0x00044655 File Offset: 0x00042855
		public ODataUndeclaredPropertyBehaviorKinds UndeclaredPropertyBehaviorKinds { get; set; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0004465E File Offset: 0x0004285E
		// (set) Token: 0x06001213 RID: 4627 RVA: 0x00044666 File Offset: 0x00042866
		public bool DisableMessageStreamDisposal { get; set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x0004466F File Offset: 0x0004286F
		// (set) Token: 0x06001215 RID: 4629 RVA: 0x00044677 File Offset: 0x00042877
		public ODataVersion MaxProtocolVersion { get; set; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00044680 File Offset: 0x00042880
		internal bool DisableStrictMetadataValidation
		{
			get
			{
				return this.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesServer || this.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesClient;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x000446A0 File Offset: 0x000428A0
		internal ODataReaderBehavior ReaderBehavior
		{
			get
			{
				return this.readerBehavior;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x000446A8 File Offset: 0x000428A8
		internal Func<ODataEntry, XmlReader, Uri, XmlReader> AtomEntryXmlCustomizationCallback
		{
			get
			{
				return this.atomFormatEntryXmlCustomizationCallback;
			}
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000446B0 File Offset: 0x000428B0
		public bool ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds)
		{
			if (undeclaredPropertyBehaviorKinds == ODataUndeclaredPropertyBehaviorKinds.None)
			{
				return this.UndeclaredPropertyBehaviorKinds == ODataUndeclaredPropertyBehaviorKinds.None;
			}
			return this.UndeclaredPropertyBehaviorKinds.HasFlag(undeclaredPropertyBehaviorKinds);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x000446CB File Offset: 0x000428CB
		public void SetAtomEntryXmlCustomizationCallback(Func<ODataEntry, XmlReader, Uri, XmlReader> atomEntryXmlCustomizationCallback)
		{
			this.atomFormatEntryXmlCustomizationCallback = atomEntryXmlCustomizationCallback;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000446D4 File Offset: 0x000428D4
		public void EnableDefaultBehavior()
		{
			this.SetAtomEntryXmlCustomizationCallback(null);
			this.readerBehavior = ODataReaderBehavior.DefaultBehavior;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000446E8 File Offset: 0x000428E8
		public void EnableWcfDataServicesServerBehavior(bool usesV1Provider)
		{
			this.SetAtomEntryXmlCustomizationCallback(null);
			this.readerBehavior = ODataReaderBehavior.CreateWcfDataServicesServerBehavior(usesV1Provider);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x000446FD File Offset: 0x000428FD
		public void EnableWcfDataServicesClientBehavior(Func<IEdmType, string, IEdmType> typeResolver, string odataNamespace, string typeScheme, Func<ODataEntry, XmlReader, Uri, XmlReader> entryXmlCustomizationCallback)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(odataNamespace, "odataNamespace");
			ExceptionUtils.CheckArgumentNotNull<string>(typeScheme, "typeScheme");
			this.SetAtomEntryXmlCustomizationCallback(entryXmlCustomizationCallback);
			this.readerBehavior = ODataReaderBehavior.CreateWcfDataServicesClientBehavior(typeResolver, odataNamespace, typeScheme);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0004472B File Offset: 0x0004292B
		[Obsolete("The 'shouldQualifyOperations' parameter is no longer needed and will be removed. Use the overload which does not take it.")]
		public void EnableWcfDataServicesClientBehavior(Func<IEdmType, string, IEdmType> typeResolver, string odataNamespace, string typeScheme, Func<ODataEntry, XmlReader, Uri, XmlReader> entryXmlCustomizationCallback, Func<IEdmEntityType, bool> shouldQualifyOperations)
		{
			this.EnableWcfDataServicesClientBehavior(typeResolver, odataNamespace, typeScheme, entryXmlCustomizationCallback);
			this.readerBehavior.OperationsBoundToEntityTypeMustBeContainerQualified = shouldQualifyOperations;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00044745 File Offset: 0x00042945
		internal bool ShouldSkipAnnotation(string annotationName)
		{
			return this.MaxProtocolVersion < ODataVersion.V3 || this.ShouldIncludeAnnotation == null || !this.ShouldIncludeAnnotation.Invoke(annotationName);
		}

		// Token: 0x040006CF RID: 1743
		private ODataReaderBehavior readerBehavior;

		// Token: 0x040006D0 RID: 1744
		private Func<ODataEntry, XmlReader, Uri, XmlReader> atomFormatEntryXmlCustomizationCallback;
	}
}
