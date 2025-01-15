using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200211C RID: 8476
	[Obsolete("This functionality is obsolete and will be removed from future version release. Please see OpenXmlValidator class for supported validation functionality.", false)]
	[Serializable]
	internal sealed class OpenXmlPackageValidationEventArgs : EventArgs
	{
		// Token: 0x0600D1BF RID: 53695 RVA: 0x0029BB65 File Offset: 0x00299D65
		internal OpenXmlPackageValidationEventArgs()
		{
		}

		// Token: 0x170032B3 RID: 12979
		// (get) Token: 0x0600D1C0 RID: 53696 RVA: 0x0029BB6D File Offset: 0x00299D6D
		// (set) Token: 0x0600D1C1 RID: 53697 RVA: 0x0029BB96 File Offset: 0x00299D96
		public string Message
		{
			get
			{
				if (this._message == null && this.MessageId != null)
				{
					return ExceptionMessages.ResourceManager.GetString(this.MessageId);
				}
				return this._message;
			}
			set
			{
				this._message = value;
			}
		}

		// Token: 0x170032B4 RID: 12980
		// (get) Token: 0x0600D1C2 RID: 53698 RVA: 0x0029BB9F File Offset: 0x00299D9F
		// (set) Token: 0x0600D1C3 RID: 53699 RVA: 0x0029BBA7 File Offset: 0x00299DA7
		public string PartClassName
		{
			get
			{
				return this._partClassName;
			}
			internal set
			{
				this._partClassName = value;
			}
		}

		// Token: 0x170032B5 RID: 12981
		// (get) Token: 0x0600D1C4 RID: 53700 RVA: 0x0029BBB0 File Offset: 0x00299DB0
		// (set) Token: 0x0600D1C5 RID: 53701 RVA: 0x0029BBB8 File Offset: 0x00299DB8
		public OpenXmlPart SubPart
		{
			get
			{
				return this._childPart;
			}
			internal set
			{
				this._childPart = value;
			}
		}

		// Token: 0x170032B6 RID: 12982
		// (get) Token: 0x0600D1C6 RID: 53702 RVA: 0x0029BBC1 File Offset: 0x00299DC1
		// (set) Token: 0x0600D1C7 RID: 53703 RVA: 0x0029BBC9 File Offset: 0x00299DC9
		public OpenXmlPart Part
		{
			get
			{
				return this._parentPart;
			}
			internal set
			{
				this._parentPart = value;
			}
		}

		// Token: 0x170032B7 RID: 12983
		// (get) Token: 0x0600D1C8 RID: 53704 RVA: 0x0029BBD2 File Offset: 0x00299DD2
		// (set) Token: 0x0600D1C9 RID: 53705 RVA: 0x0029BBDA File Offset: 0x00299DDA
		internal string MessageId { get; set; }

		// Token: 0x170032B8 RID: 12984
		// (get) Token: 0x0600D1CA RID: 53706 RVA: 0x0029BBE3 File Offset: 0x00299DE3
		// (set) Token: 0x0600D1CB RID: 53707 RVA: 0x0029BBEB File Offset: 0x00299DEB
		internal DataPartReferenceRelationship DataPartReferenceRelationship { get; set; }

		// Token: 0x04006957 RID: 26967
		private string _message;

		// Token: 0x04006958 RID: 26968
		private string _partClassName;

		// Token: 0x04006959 RID: 26969
		[NonSerialized]
		private OpenXmlPart _childPart;

		// Token: 0x0400695A RID: 26970
		[NonSerialized]
		private OpenXmlPart _parentPart;
	}
}
