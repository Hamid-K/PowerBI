using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Hosting;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200009D RID: 157
	[Guid("92668AA5-FE9A-49ea-BE65-45F19305EDC5")]
	[Designer("Microsoft.AnalysisServices.Design.MajorObjectDesigner, Microsoft.AnalysisServices.Design.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91")]
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736AD6E5F9586BAC2D531EABC3ACC666C2F8EC879FA94F8F7B0327D2FF2ED523448F83C3D5C5DD2DFC7BC99C5286B2C125117BF5CBE242B9D41750732B2BDFFE649C6EFB8E5526D526FDD130095ECDB7BF210809C6CDAD8824FAA9AC0310AC3CBA2AA0523567B2DFA7FE250B30FACBD62D4EC99B94AC47C7D3B28F1F6E4C8")]
	public abstract class MajorObject : NamedComponent, IDeserializationStartCallback, IDeserializationCallback
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x00024F22 File Offset: 0x00023122
		void IDeserializationStartCallback.OnDeserializationBegin(object sender)
		{
			this.internalState = MajorObjectState.Loading;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00024F2C File Offset: 0x0002312C
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.internalState = MajorObjectState.Ready;
			IList owningCollection = base.OwningCollection;
			this.AfterInsert((owningCollection == null) ? (-1) : owningCollection.IndexOf(this));
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00024F5A File Offset: 0x0002315A
		protected MajorObject()
		{
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00024F78 File Offset: 0x00023178
		protected MajorObject(string name)
			: base(name)
		{
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00024F97 File Offset: 0x00023197
		protected MajorObject(string name, string id)
			: base(name, id)
		{
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00024FB7 File Offset: 0x000231B7
		[XmlIgnore]
		[Browsable(false)]
		public bool IsLoaded
		{
			get
			{
				return this.body != null;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00024FC4 File Offset: 0x000231C4
		[XmlIgnore]
		[Browsable(false)]
		internal MajorObjectState InternalState
		{
			get
			{
				if (this.internalState == MajorObjectState.Loading)
				{
					return MajorObjectState.Loading;
				}
				MajorObject majorObject = base.Parent as MajorObject;
				if (majorObject != null)
				{
					return majorObject.InternalState;
				}
				return MajorObjectState.Ready;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00024FF3 File Offset: 0x000231F3
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x00024FFB File Offset: 0x000231FB
		[Browsable(false)]
		[LocalizedCategory("PropertyCategory_Advanced")]
		public DateTime CreatedTimestamp
		{
			get
			{
				return this.createdTimestamp;
			}
			set
			{
				this.createdTimestamp = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00025004 File Offset: 0x00023204
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0002500C File Offset: 0x0002320C
		[ReadOnly(true)]
		[LocalizedCategory("PropertyCategory_Advanced")]
		[LocalizedDescription("PropertyDescription_MajorObject_LastSchemaUpdate")]
		public DateTime LastSchemaUpdate
		{
			get
			{
				return this.lastSchemaUpdate;
			}
			set
			{
				this.lastSchemaUpdate = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00025015 File Offset: 0x00023215
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x00025024 File Offset: 0x00023224
		[XmlElement(IsNullable = false)]
		public override string Description
		{
			get
			{
				this.CheckBody();
				return base.Description;
			}
			set
			{
				this.CheckBody();
				base.Description = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x00025034 File Offset: 0x00023234
		[Browsable(false)]
		[XmlArray(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
		public override AnnotationCollection Annotations
		{
			get
			{
				this.CheckBody();
				return base.Annotations;
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00025043 File Offset: 0x00023243
		internal virtual void OnBeforeRefresh(ObjectExpansion expansion, RefreshType? refreshType = null)
		{
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00025045 File Offset: 0x00023245
		internal virtual void OnAfterRefresh(ObjectExpansion expansion, RefreshType? refreshType = null)
		{
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00025047 File Offset: 0x00023247
		internal bool IsInRefresh()
		{
			return Interlocked.CompareExchange(ref this.refreshCounter, 0, 0) != 0;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0002505C File Offset: 0x0002325C
		public void Refresh()
		{
			Interlocked.Increment(ref this.refreshCounter);
			try
			{
				this.OnBeforeRefresh(ObjectExpansion.Partial, null);
				Server.SendRefresh(this as IMajorObject, ObjectExpansion.Partial);
				this.OnAfterRefresh(ObjectExpansion.Partial, null);
			}
			finally
			{
				Interlocked.Decrement(ref this.refreshCounter);
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000250C0 File Offset: 0x000232C0
		internal void Refresh(ObjectExpansion expansion)
		{
			Interlocked.Increment(ref this.refreshCounter);
			try
			{
				this.OnBeforeRefresh(expansion, null);
				Server.SendRefresh(this as IMajorObject, expansion);
				this.OnAfterRefresh(expansion, null);
			}
			finally
			{
				Interlocked.Decrement(ref this.refreshCounter);
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00025124 File Offset: 0x00023324
		public void Refresh(bool full)
		{
			Interlocked.Increment(ref this.refreshCounter);
			try
			{
				ObjectExpansion objectExpansion = (full ? ObjectExpansion.Full : ObjectExpansion.Partial);
				this.OnBeforeRefresh(objectExpansion, null);
				Server.SendRefresh(this as IMajorObject, objectExpansion);
				this.OnAfterRefresh(objectExpansion, null);
			}
			finally
			{
				Interlocked.Decrement(ref this.refreshCounter);
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00025190 File Offset: 0x00023390
		public void Refresh(bool full, RefreshType type)
		{
			Interlocked.Increment(ref this.refreshCounter);
			try
			{
				this.OnBeforeRefresh(full ? ObjectExpansion.Full : ObjectExpansion.Partial, new RefreshType?(type));
				if (full)
				{
					if (type == RefreshType.LoadedObjectsOnly && this.IsLoaded)
					{
						Server.SendRefresh(this as IMajorObject, ObjectExpansion.Partial);
						this.RefreshMajorChildren(RefreshType.LoadedObjectsOnly);
					}
					else if (type == RefreshType.UnloadedObjectsOnly)
					{
						if (!this.IsLoaded)
						{
							Server.SendRefresh(this as IMajorObject, ObjectExpansion.Full);
						}
						else
						{
							this.RefreshMajorChildren(RefreshType.UnloadedObjectsOnly);
						}
					}
				}
				else if (type == RefreshType.LoadedObjectsOnly && this.IsLoaded)
				{
					Server.SendRefresh(this as IMajorObject, ObjectExpansion.Partial);
				}
				else if (type == RefreshType.UnloadedObjectsOnly && !this.IsLoaded)
				{
					Server.SendRefresh(this as IMajorObject, ObjectExpansion.Partial);
				}
				this.OnAfterRefresh(full ? ObjectExpansion.Full : ObjectExpansion.Partial, new RefreshType?(type));
			}
			finally
			{
				Interlocked.Decrement(ref this.refreshCounter);
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00025268 File Offset: 0x00023468
		internal virtual void RefreshMajorChildren(RefreshType type)
		{
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0002526A File Offset: 0x0002346A
		internal virtual void OnAfterUpdate(UpdateOptions options)
		{
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0002526C File Offset: 0x0002346C
		public void Update()
		{
			this.Update(UpdateOptions.Default, UpdateMode.Default, null);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00025277 File Offset: 0x00023477
		public void Update(UpdateOptions options)
		{
			this.Update(options, UpdateMode.Default, null);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00025282 File Offset: 0x00023482
		public void Update(UpdateOptions options, UpdateMode mode)
		{
			this.Update(options, mode, null);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0002528D File Offset: 0x0002348D
		public void Update(UpdateOptions options, UpdateMode mode, XmlaWarningCollection warnings)
		{
			Server.SendUpdate((IMajorObject)this, options, mode, warnings, null);
			this.OnAfterUpdate(options);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000252A5 File Offset: 0x000234A5
		public void Update(UpdateOptions options, UpdateMode mode, XmlaWarningCollection warnings, ImpactDetailCollection impactResult)
		{
			this.Update(options, mode, warnings, impactResult, false);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000252B3 File Offset: 0x000234B3
		public void Update(UpdateOptions options, UpdateMode mode, XmlaWarningCollection warnings, ImpactDetailCollection impactResult, bool analyzeImpactOnly)
		{
			if (impactResult == null)
			{
				throw new ArgumentNullException("impactResult");
			}
			Server.SendUpdate((IMajorObject)this, options, mode, warnings, impactResult);
			if (!analyzeImpactOnly)
			{
				Server.SendUpdate((IMajorObject)this, options, mode, warnings, null);
			}
			this.OnAfterUpdate(options);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000252EE File Offset: 0x000234EE
		internal virtual void OnAfterDrop()
		{
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000252F0 File Offset: 0x000234F0
		public void Drop()
		{
			this.DropPrivate(DropOptions.Default, null, null, false);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000252FC File Offset: 0x000234FC
		public void Drop(DropOptions options)
		{
			this.DropPrivate(options, null, null, false);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00025308 File Offset: 0x00023508
		public void Drop(DropOptions options, XmlaWarningCollection warnings)
		{
			this.DropPrivate(options, warnings, null, false);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00025314 File Offset: 0x00023514
		public void Drop(DropOptions options, XmlaWarningCollection warnings, ImpactDetailCollection impactResult)
		{
			this.Drop(options, warnings, impactResult, false);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00025320 File Offset: 0x00023520
		public void Drop(DropOptions options, XmlaWarningCollection warnings, ImpactDetailCollection impactResult, bool analyzeImpactOnly)
		{
			if (impactResult == null)
			{
				throw new ArgumentNullException("impactResult");
			}
			this.DropPrivate(options, warnings, impactResult, analyzeImpactOnly);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0002533C File Offset: 0x0002353C
		private void DropPrivate(DropOptions options, XmlaWarningCollection warnings, ImpactDetailCollection impactResult, bool analyzeImpactOnly)
		{
			ModelComponentCollection owningCollection = base.OwningCollection;
			Server parentServer = ((IMajorObject)this).ParentServer;
			if (parentServer == null || !parentServer.Connected)
			{
				throw new InvalidOperationException(SR.MajorObject_Delete_NoConnectedParentServer(base.GetType().Name, base.Name));
			}
			bool flag = (options & DropOptions.AlterOrDeleteDependents) > DropOptions.Default;
			bool flag2 = (options & DropOptions.IgnoreFailures) > DropOptions.Default;
			if (flag)
			{
				XmlaClient connection = parentServer.Connection;
				lock (connection)
				{
					Hashtable hashtable = new Hashtable();
					Hashtable hashtable2 = new Hashtable();
					ArrayList arrayList = new ArrayList();
					ArrayList arrayList2 = new ArrayList();
					Hashtable hashtable3 = new Hashtable();
					Hashtable hashtable4 = new Hashtable();
					ImpactDetailCollection impactDetailCollection = new ImpactDetailCollection();
					bool captureXml = parentServer.CaptureXml;
					parentServer.CaptureXml = false;
					try
					{
						parentServer.Delete((IMajorObject)this, impactDetailCollection, flag2);
					}
					finally
					{
						parentServer.CaptureXml = captureXml;
					}
					this.GetDropDependents(hashtable, hashtable2);
					foreach (object obj in impactDetailCollection.GetInvalidObjects())
					{
						MajorObject majorObject = (MajorObject)obj;
						if (hashtable2.Contains(majorObject))
						{
							arrayList2.Add(((IMajorObject)majorObject).ObjectReference);
						}
						else if (hashtable.Contains(majorObject))
						{
							arrayList.Add(majorObject);
						}
					}
					arrayList2.Add(((IMajorObject)this).ObjectReference);
					foreach (object obj2 in hashtable.Keys)
					{
						MajorObject majorObject2 = (MajorObject)obj2;
						hashtable3.Add(majorObject2, majorObject2.Clone(true));
					}
					foreach (object obj3 in hashtable2.Keys)
					{
						MajorObject majorObject3 = (MajorObject)obj3;
						hashtable4.Add(majorObject3, majorObject3.OwningCollection);
					}
					owningCollection.Remove(this);
					foreach (object obj4 in hashtable2.Keys)
					{
						MajorObject majorObject4 = (MajorObject)obj4;
						if (majorObject4.OwningCollection != null)
						{
							majorObject4.OwningCollection.Remove(majorObject4);
						}
					}
					try
					{
						parentServer.SendBatch(arrayList2, arrayList);
						goto IL_0367;
					}
					catch
					{
						owningCollection.Add(this);
						foreach (object obj5 in hashtable4)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj5;
							((MajorObjectCollection)dictionaryEntry.Value).Add((MajorObject)dictionaryEntry.Key);
						}
						foreach (object obj6 in hashtable3)
						{
							DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj6;
							((MajorObject)dictionaryEntry2.Value).CopyTo((MajorObject)dictionaryEntry2.Key, true);
						}
						throw;
					}
				}
			}
			if (impactResult == null)
			{
				parentServer.Delete((IMajorObject)this, null, flag2);
				owningCollection.Remove(this);
			}
			else if (analyzeImpactOnly)
			{
				parentServer.Delete((IMajorObject)this, impactResult, flag2);
			}
			else
			{
				parentServer.Delete((IMajorObject)this, impactResult, flag2);
				parentServer.Delete((IMajorObject)this, null, flag2);
				owningCollection.Remove(this);
			}
			IL_0367:
			this.OnAfterDrop();
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00025794 File Offset: 0x00023994
		internal bool CheckBody()
		{
			return this.EnsureBody() != null;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0002579F File Offset: 0x0002399F
		internal void CreateBody()
		{
			this.body = this.CreateBodyImpl();
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000257AD File Offset: 0x000239AD
		internal void ResetBody()
		{
			this.body = null;
		}

		// Token: 0x0600077F RID: 1919
		private protected abstract MajorObject.MajorObjectBody CreateBodyImpl();

		// Token: 0x06000780 RID: 1920 RVA: 0x000257B6 File Offset: 0x000239B6
		private protected TBody GetBody<TBody>() where TBody : MajorObject.MajorObjectBody
		{
			return (TBody)((object)this.EnsureBody());
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000257C4 File Offset: 0x000239C4
		private MajorObject.MajorObjectBody EnsureBody()
		{
			if (this.body == null)
			{
				if (this.InternalState == MajorObjectState.Loading)
				{
					this.CreateBody();
				}
				else
				{
					Server parentServer = ((IMajorObject)this).ParentServer;
					if (parentServer == null || !parentServer.Connected)
					{
						this.CreateBody();
					}
					else
					{
						this.Refresh();
					}
				}
			}
			return this.body;
		}

		// Token: 0x06000782 RID: 1922
		protected internal abstract MajorObject Clone(bool forceBodyLoading);

		// Token: 0x06000783 RID: 1923 RVA: 0x00025818 File Offset: 0x00023A18
		protected internal virtual void CopyTo(MajorObject destination, bool forceBodyLoading)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destination == null)
			{
				throw new ArgumentException(SR.Copy_InvalidDestination, "destination");
			}
			if (destination.OwningCollection == null)
			{
				destination.ID = base.ID;
			}
			destination.Name = base.Name;
			destination.CreatedTimestamp = this.CreatedTimestamp;
			destination.LastSchemaUpdate = this.LastSchemaUpdate;
			if (!this.IsLoaded && !forceBodyLoading)
			{
				return;
			}
			if (!destination.IsLoaded)
			{
				destination.CreateBody();
			}
			destination.Description = this.Description;
			this.Annotations.CopyTo(destination.Annotations);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000258B8 File Offset: 0x00023AB8
		public Hashtable GetUpdateOverwrites(bool fullExpansion)
		{
			Hashtable hashtable = new Hashtable();
			ImpactDetailCollection impactDetailCollection = new ImpactDetailCollection();
			Server parentServer = ((IMajorObject)this).ParentServer;
			this.Update(fullExpansion ? UpdateOptions.ExpandFull : UpdateOptions.Default, UpdateMode.Default, null, impactDetailCollection, true);
			foreach (object obj in ((IEnumerable)impactDetailCollection))
			{
				IMajorObject majorObject = ((ImpactDetail)obj).Object as IMajorObject;
				if (majorObject != null)
				{
					DateTime dateTime = parentServer.GetLastSchemaUpdate(majorObject);
					if (!((MajorObject)majorObject).LastSchemaUpdate.Equals(dateTime))
					{
						hashtable[majorObject] = dateTime;
					}
				}
			}
			if (!hashtable.Contains(this))
			{
				DateTime dateTime2 = parentServer.GetLastSchemaUpdate((IMajorObject)this);
				if (!this.LastSchemaUpdate.Equals(dateTime2))
				{
					hashtable.Add(this, dateTime2);
				}
			}
			if (hashtable.Count != 0)
			{
				return hashtable;
			}
			return null;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000259B8 File Offset: 0x00023BB8
		public virtual Hashtable GetDependents(Hashtable dependents)
		{
			return dependents;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000259BC File Offset: 0x00023BBC
		public virtual Hashtable GetReferences(Hashtable references, bool forMajorChildrenAlso)
		{
			if (references == null)
			{
				references = new Hashtable();
			}
			MajorObject majorObject = base.Parent as MajorObject;
			if (majorObject != null)
			{
				references[majorObject] = null;
			}
			return references;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000259EC File Offset: 0x00023BEC
		internal object GetConfigurationValue(string settingName, string[] indexes, object defaultValue)
		{
			IHostConfigurationSettings hostConfigurationSettings = base.GetHostService(typeof(IHostConfigurationSettings)) as IHostConfigurationSettings;
			if (hostConfigurationSettings == null)
			{
				return defaultValue;
			}
			object obj = hostConfigurationSettings.GetSetting(settingName, indexes);
			if (this.InternalState == MajorObjectState.Ready && obj == null && defaultValue != null)
			{
				hostConfigurationSettings.SetSetting(settingName, defaultValue, indexes);
				obj = defaultValue;
			}
			return obj;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00025A38 File Offset: 0x00023C38
		internal bool SetConfigurationValue(string settingName, string[] indexes, object configValue)
		{
			IHostConfigurationSettings hostConfigurationSettings = base.GetHostService(typeof(IHostConfigurationSettings)) as IHostConfigurationSettings;
			if (hostConfigurationSettings == null)
			{
				return false;
			}
			if (MajorObjectState.Loading == this.InternalState)
			{
				if (hostConfigurationSettings.GetSetting(settingName, indexes) == null)
				{
					hostConfigurationSettings.SetSetting(settingName, configValue, indexes);
				}
				return false;
			}
			hostConfigurationSettings.SetSetting(settingName, configValue, indexes);
			return true;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00025A88 File Offset: 0x00023C88
		internal virtual object GetErrorConfigurationValue(ErrorConfiguration errorConfiguration, object defaultValue)
		{
			return defaultValue;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00025A8B File Offset: 0x00023C8B
		internal virtual void SetErrorConfigurationValue(ErrorConfiguration errorConfiguration, string value)
		{
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00025A8D File Offset: 0x00023C8D
		protected internal virtual void GetCreateReferences(Hashtable createReferences, bool considerPermissions, bool considerPartitions)
		{
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00025A8F File Offset: 0x00023C8F
		protected internal virtual void GetDropDependents(Hashtable dependentsToAlter, Hashtable dependentsToDrop)
		{
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00025A94 File Offset: 0x00023C94
		internal bool HasParentsIn(Hashtable hashtable)
		{
			if (hashtable == null)
			{
				return false;
			}
			IModelComponent modelComponent = base.Parent;
			int num = 0;
			while (num < 10 && modelComponent != null)
			{
				if (hashtable.Contains(modelComponent))
				{
					return true;
				}
				num++;
				modelComponent = modelComponent.Parent;
			}
			return false;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00025AD0 File Offset: 0x00023CD0
		internal bool IsBodyLoadable()
		{
			return this.body != null || ((IMajorObject)this).ParentServer != null;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00025AEA File Offset: 0x00023CEA
		public bool Validate(ValidationResultCollection results)
		{
			return this.Validate(results, ValidationOptions.None);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00025AF4 File Offset: 0x00023CF4
		public bool Validate(ValidationResultCollection results, ValidationOptions flags)
		{
			Server server = this as Server;
			IModelComponent modelComponent = base.Parent;
			while (server == null && modelComponent != null)
			{
				server = modelComponent as Server;
				modelComponent = modelComponent.Parent;
			}
			if (server == null)
			{
				throw new AmoException(ValidationSR.ParentServerIsMissing);
			}
			return this.Validate(results, flags, server.Edition);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00025B44 File Offset: 0x00023D44
		public bool Validate(ValidationResultCollection results, ValidationOptions flags, ServerEdition serverEdition)
		{
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
			ValidationErrorCollection validationErrorCollection = new ValidationErrorCollection();
			bool flag = this.Validate(validationErrorCollection, (flags & ValidationOptions.AddDetails) > ValidationOptions.None, serverEdition);
			int i = 0;
			int count = validationErrorCollection.Count;
			while (i < count)
			{
				ValidationError validationError = validationErrorCollection[i];
				if (validationError.Priority == ErrorPriority.High)
				{
					results.AddError((ModelComponent)validationError.Source, validationError.ErrorText);
				}
				else
				{
					results.AddErrorMedium((ModelComponent)validationError.Source, validationError.ErrorText);
				}
				i++;
			}
			if ((flags & ValidationOptions.AddWarnings) != ValidationOptions.None || (flags & ValidationOptions.AddMessages) != ValidationOptions.None)
			{
				Database parentDatabase = ((IMajorObject)this).ParentDatabase;
				this.Validate(results, flags, serverEdition, (parentDatabase == null) ? null : parentDatabase.DismissedValidationRules, (parentDatabase == null) ? null : parentDatabase.DismissedValidationResults);
			}
			return flag;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00025C0A File Offset: 0x00023E0A
		internal virtual void Validate(ValidationResultCollection results, ValidationOptions flags, ServerEdition serverEdition, DismissedValidationRuleCollection dismissedRules, DismissedValidationResultCollection dismissedResults)
		{
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00025C0C File Offset: 0x00023E0C
		internal virtual Type GetBaseType()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00025C13 File Offset: 0x00023E13
		internal virtual Microsoft.AnalysisServices.Core.IObjectReference GetObjectReference()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400048C RID: 1164
		private protected const bool AnalyzeImpactOnlyDefault = false;

		// Token: 0x0400048D RID: 1165
		private MajorObject.MajorObjectBody body;

		// Token: 0x0400048E RID: 1166
		internal MajorObjectState internalState;

		// Token: 0x0400048F RID: 1167
		private DateTime createdTimestamp = DateTime.MinValue;

		// Token: 0x04000490 RID: 1168
		private DateTime lastSchemaUpdate = DateTime.MinValue;

		// Token: 0x04000491 RID: 1169
		private int refreshCounter;

		// Token: 0x02000199 RID: 409
		internal abstract class MajorObjectBody
		{
			// Token: 0x06001301 RID: 4865 RVA: 0x000430FC File Offset: 0x000412FC
			private protected MajorObjectBody(MajorObject owner)
			{
				this.Owner = owner;
			}

			// Token: 0x17000628 RID: 1576
			// (get) Token: 0x06001302 RID: 4866 RVA: 0x0004310B File Offset: 0x0004130B
			public MajorObject Owner { get; }
		}
	}
}
