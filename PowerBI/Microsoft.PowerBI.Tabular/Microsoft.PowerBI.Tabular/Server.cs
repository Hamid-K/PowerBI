using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Extensions;
using Microsoft.AnalysisServices.Tabular.DDL;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000D8 RID: 216
	public class Server : Server, ITxService, ICloneable, IMajorObject
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0006EEAB File Offset: 0x0006D0AB
		Database IMajorObject.ParentDatabase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0006EEAE File Offset: 0x0006D0AE
		Server IMajorObject.ParentServer
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0006EEB1 File Offset: 0x0006D0B1
		void IMajorObject.WriteRef(XmlWriter writer)
		{
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0006EEB3 File Offset: 0x0006D0B3
		Type IMajorObject.BaseType
		{
			get
			{
				return this.GetBaseType();
			}
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0006EEBB File Offset: 0x0006D0BB
		void IMajorObject.CreateBody()
		{
			base.CreateBody();
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0006EEC3 File Offset: 0x0006D0C3
		string IMajorObject.Path
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x0006EECA File Offset: 0x0006D0CA
		ObjectReference IMajorObject.ObjectReference
		{
			get
			{
				return (ObjectReference)this.GetObjectReference();
			}
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0006EED7 File Offset: 0x0006D0D7
		bool IMajorObject.DependsOn(IMajorObject obj)
		{
			return false;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0006EEDA File Offset: 0x0006D0DA
		internal override IObjectReference GetObjectReference()
		{
			return new ObjectReference();
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0006EEE1 File Offset: 0x0006D0E1
		internal override Type GetBaseType()
		{
			return typeof(Server);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0006EEED File Offset: 0x0006D0ED
		private protected override MajorObject.MajorObjectBody CreateBodyImpl()
		{
			return new Server.ServerBody(this);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0006EEF5 File Offset: 0x0006D0F5
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0006EF00 File Offset: 0x0006D100
		protected internal override MajorObject Clone(bool forceBodyLoading)
		{
			MajorObject majorObject = new Server();
			this.CopyTo(majorObject, forceBodyLoading);
			return majorObject;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0006EF1C File Offset: 0x0006D11C
		public Server()
		{
			this.analysisServicesClient = new AnalysisServicesClient(this, this.captureLog);
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0006EF41 File Offset: 0x0006D141
		[XmlIgnore]
		public SessionTrace SessionTrace
		{
			get
			{
				return this.sessionTrace;
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0006EF4C File Offset: 0x0006D14C
		public Server Clone()
		{
			string connectionString = base.ConnectionString;
			Server server = new Server();
			if (connectionString != null && connectionString.Length > 0)
			{
				server.analysisServicesClient.SetConnectionInfo(new ConnectionInfo(connectionString));
			}
			server.CaptureXml = base.CaptureXml;
			server.analysisServicesClient.SessionID = base.SessionID;
			Utils.Copy(base.CaptureLog, server.CaptureLog);
			return this.CopyTo(server);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0006EFB9 File Offset: 0x0006D1B9
		public Server CopyTo(Server obj)
		{
			this.CopyTo(obj, true);
			return obj;
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0006EFC4 File Offset: 0x0006D1C4
		[XmlIgnore]
		public bool IsInTransaction
		{
			get
			{
				return base.IsInTransactionInternal();
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0006EFCC File Offset: 0x0006D1CC
		public void BeginTransaction()
		{
			this.CheckIfConnected(false);
			if (!this.IsInTransaction && base.IsLoaded && this.AnyTMDatabaseHasLocalChangesImpl(null))
			{
				throw new InvalidOperationException(TomSR.Exception_CannotStartTransactionLocalchanges);
			}
			if (base.CurrentTransaction == null)
			{
				this.CreateTransaction(null);
				return;
			}
			base.BeginTransactionInternal();
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0006F01A File Offset: 0x0006D21A
		void ITxService.BeginTransaction(Database initiatingDB)
		{
			if (this.AnyTMDatabaseHasLocalChangesImpl((Database)initiatingDB))
			{
				throw new InvalidOperationException(TomSR.Exception_CannotStartTransactionLocalchanges);
			}
			this.CreateTransaction(initiatingDB);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0006F03C File Offset: 0x0006D23C
		internal override bool AnyTMDatabaseHasLocalChanges()
		{
			return base.IsLoaded && this.AnyTMDatabaseHasLocalChangesImpl(null);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0006F050 File Offset: 0x0006D250
		internal bool AnyTMDatabaseHasLocalChangesImpl(Database dbWithAllowedLocalChanges)
		{
			foreach (object obj in this.Databases)
			{
				Database database = (Database)obj;
				if (database.IsLoaded && database.IsTM && (database.HasLocalBodyChanges() || (database.IsModelLoaded() && database.Model.HasLocalChanges)) && database != dbWithAllowedLocalChanges)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0006F0DC File Offset: 0x0006D2DC
		private void CreateTransaction(Database modifiedDB)
		{
			base.CurrentTransaction = new Transaction(this)
			{
				ModifiedDatabase = modifiedDB
			};
			try
			{
				base.BeginTransactionInternal();
			}
			catch (Exception)
			{
				Transaction.Cleanup(base.CurrentTransaction, false);
				throw;
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0006F124 File Offset: 0x0006D324
		internal override string TryLookupDatabaseId(string databaseName)
		{
			if (!string.IsNullOrEmpty(databaseName))
			{
				if (!base.IsLoaded)
				{
					base.Refresh(ObjectExpansion.Partial);
				}
				if (this.Databases.Count != 0)
				{
					Database database = this.Databases.FindByName(databaseName);
					if (database != null)
					{
						return database.ID;
					}
				}
			}
			return null;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0006F16D File Offset: 0x0006D36D
		public void CommitTransaction()
		{
			this.CommitTransactionPrivate();
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0006F176 File Offset: 0x0006D376
		public void CommitTransaction(out ModelOperationResult modelResult)
		{
			modelResult = this.CommitTransactionPrivate();
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0006F180 File Offset: 0x0006D380
		IModelOperationResult ITxService.CommitTransactionWithResult()
		{
			return this.CommitTransactionPrivate();
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0006F188 File Offset: 0x0006D388
		internal ModelOperationResult CommitTransactionPrivate()
		{
			ObjectImpact objectImpact = ObjectImpact.Empty;
			Model model = null;
			if (base.CurrentTransaction != null && base.CurrentTransaction.ModifiedDatabase != null && base.CurrentTransaction.ModifiedDatabase.IsModelLoaded())
			{
				model = ((Database)base.CurrentTransaction.ModifiedDatabase).Model;
			}
			bool flag = model != null && !model.IsRemoved;
			long num = (flag ? model.Version : (-1000L));
			string text = new XElement(XmlaConstants.XNS.ddl + "CommitTransaction").ToString();
			ImpactDataSet impactDataSet;
			XmlaResultCollection xmlaResultCollection = ExecuteUtil.RunCommand(text, flag, num, this, out impactDataSet);
			if (xmlaResultCollection != null && xmlaResultCollection.ContainsErrors)
			{
				throw new OperationException(TomSR.Exception_CommitTransactionFailed(xmlaResultCollection.GetAggregatedMessage()), xmlaResultCollection, text);
			}
			if (flag && !base.CaptureXml)
			{
				model.ApplyImpact(impactDataSet);
			}
			if (this.transactionsCount > 1)
			{
				this.transactionsCount--;
			}
			else
			{
				Transaction.Cleanup(base.CurrentTransaction, false);
				if (model != null && !model.IsRemoved)
				{
					objectImpact = model.CompleteTransaction();
				}
				foreach (object obj in this.Databases)
				{
					Database database = (Database)obj;
					if (database.IsLoaded && database.HasLocalBodyChanges())
					{
						database.ResetLocalBodyChangesIndication();
					}
				}
			}
			return new ModelOperationResult
			{
				Impact = objectImpact,
				XmlaResults = xmlaResultCollection
			};
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0006F30C File Offset: 0x0006D50C
		public void RollbackTransaction()
		{
			if (!this.IsInTransaction)
			{
				throw new InvalidOperationException(TomSR.Exception_NoActiveTransactionInSession);
			}
			try
			{
				base.RollbackTransactionInternal();
			}
			catch (ConnectionException)
			{
				throw;
			}
			catch (OperationException)
			{
			}
			catch (Exception)
			{
				if (!base.Connected)
				{
					throw;
				}
			}
			Transaction.Cleanup(base.CurrentTransaction, true);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0006F37C File Offset: 0x0006D57C
		internal override void CancelTransactionInternal()
		{
			Transaction.Cleanup(base.CurrentTransaction, true);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0006F38C File Offset: 0x0006D58C
		public override void Disconnect(bool endSession)
		{
			try
			{
				Transaction.Cleanup(base.CurrentTransaction, false);
				if (this.sessionTrace != null)
				{
					if (this.sessionTrace.IsStarted)
					{
						this.sessionTrace.Stop();
					}
					this.sessionTrace.Dispose();
					this.sessionTrace = null;
				}
				if (base.IsLoaded)
				{
					foreach (object obj in this.Traces)
					{
						Trace trace = (Trace)obj;
						if (trace.IsStarted)
						{
							trace.Stop();
						}
					}
				}
				if (endSession)
				{
					AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
					lock (analysisServicesClient)
					{
						this.analysisServicesClient.Disconnect(true);
						return;
					}
				}
				this.analysisServicesClient.Disconnect(false);
			}
			finally
			{
				base.Reset();
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0006F490 File Offset: 0x0006D690
		internal override void OnBeforeRefresh(ObjectExpansion expansion, RefreshType? refreshType = null)
		{
			if (base.IsLoaded)
			{
				foreach (object obj in this.Databases)
				{
					Database database = (Database)obj;
					if (database.HasObsoleteTransaction() && expansion == ObjectExpansion.Full)
					{
						database.ResetObsoleteTrasactionOnParentFullRefresh();
					}
					if (expansion != ObjectExpansion.Full)
					{
						if (expansion == ObjectExpansion.Partial)
						{
							database.OnBeforeRefresh(ObjectExpansion.ObjectProperties, refreshType);
						}
					}
					else
					{
						database.OnBeforeRefresh(ObjectExpansion.Full, refreshType);
					}
				}
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0006F518 File Offset: 0x0006D718
		internal override void OnAfterRefresh(ObjectExpansion expansion, RefreshType? refreshType = null)
		{
			if (base.IsLoaded)
			{
				foreach (object obj in this.Databases)
				{
					Database database = (Database)obj;
					if (expansion != ObjectExpansion.Full)
					{
						if (expansion == ObjectExpansion.Partial)
						{
							database.OnAfterRefresh(ObjectExpansion.ReferenceOnly, refreshType);
						}
					}
					else
					{
						database.OnAfterRefresh(ObjectExpansion.Full, refreshType);
					}
				}
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0006F590 File Offset: 0x0006D790
		internal override MajorObject GetReferenceMajorObject(IObjectReference objectReference)
		{
			return (MajorObject)((ObjectReference)objectReference).ResolveReference(this);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0006F5A3 File Offset: 0x0006D7A3
		[Browsable(false)]
		public DateTime GetLastSchemaUpdate(IMajorObject obj)
		{
			return base.GetLastSchemaUpdate((IMajorObject)obj);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0006F5B1 File Offset: 0x0006D7B1
		public void UpdateObjects(IMajorObject[] objects)
		{
			this.UpdateObjects(objects, null);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0006F5BC File Offset: 0x0006D7BC
		public void UpdateObjects(IMajorObject[] objects, ImpactDetailCollection impactResult)
		{
			if (objects == null)
			{
				throw new ArgumentNullException("objects");
			}
			int num = ((impactResult == null) ? 0 : impactResult.Count);
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				foreach (IMajorObject majorObject in objects)
				{
					if (majorObject != null && majorObject.ParentServer == this)
					{
						majorObject.Refresh(true, RefreshType.UnloadedObjectsOnly);
					}
				}
				this.analysisServicesClient.Alter(this.OrderDependentObjects(objects.Cast<IMajorObject>().ToArray<IMajorObject>()), impactResult, true, SerializationUtil.Utility.GetSerializer(ObjectExpansion.Full));
			}
			if (impactResult != null)
			{
				this.ResolveImpactReferences(impactResult, num);
				return;
			}
			foreach (IMajorObject obj in objects)
			{
				if (obj != null)
				{
					object syncRoot = this.pendingCreations.SyncRoot;
					lock (syncRoot)
					{
						this.pendingCreations.Remove(obj);
					}
				}
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0006F6DC File Offset: 0x0006D8DC
		public override bool Validate(ValidationErrorCollection errors, bool includeDetailedErrors, ServerEdition serverEdition)
		{
			if (!base.Validate(errors, includeDetailedErrors, serverEdition))
			{
				return false;
			}
			int count = errors.Count;
			if ((serverEdition == ServerEdition.LocalCube || serverEdition == ServerEdition.LocalCube64) && this.Assemblies.Count > 0)
			{
				errors.Add(this, ValidationSR.Server_AssemblyNotAllowed);
			}
			return count == errors.Count;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0006F728 File Offset: 0x0006D928
		internal override void CheckIfConnected(bool offlineCaptureXmlAllowed = false)
		{
			if (!base.Connected && (!base.CaptureXml || !offlineCaptureXmlAllowed))
			{
				throw new InvalidOperationException(SR.Server_IsNotConnected);
			}
			bool connected = base.Connected;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0006F74E File Offset: 0x0006D94E
		internal override void CreateSessionTrace()
		{
			this.sessionTrace = new SessionTrace(this);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0006F75C File Offset: 0x0006D95C
		internal override IMajorObject[] OrderDependentObjects(IMajorObject[] objects)
		{
			foreach (IMajorObject majorObject in objects)
			{
			}
			IMajorObject[] array = DependenciesCalculator.OrderObjects(objects.Cast<IMajorObject>().ToArray<IMajorObject>());
			foreach (IMajorObject majorObject2 in array)
			{
			}
			return array.Cast<IMajorObject>().ToArray<IMajorObject>();
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0006F7B2 File Offset: 0x0006D9B2
		internal override void ValidateAssemblies(ValidationErrorCollection errors, ServerEdition serverEdition)
		{
			if ((serverEdition == ServerEdition.LocalCube || serverEdition == ServerEdition.LocalCube64) && this.Assemblies.Count > 0)
			{
				errors.Add(this, ValidationSR.Server_AssemblyNotAllowed);
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0006F7D8 File Offset: 0x0006D9D8
		internal override void ResolveImpactReferences(ImpactDetailCollection impacts, int startIndex)
		{
			int i = startIndex;
			int count = impacts.Count;
			while (i < count)
			{
				ImpactDetail impactDetail = impacts[i];
				if (impactDetail.ObjectReference == null)
				{
					throw new ArgumentException(SR.ImpactAnalysisResult_ObjectReferenceMissing);
				}
				impactDetail.obj = this.GetReferenceMajorObject(impactDetail.ObjectReference);
				if (impactDetail.Object == null)
				{
					throw new OutOfSyncException((ObjectReference)impactDetail.ObjectReference);
				}
				i++;
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0006F83E File Offset: 0x0006DA3E
		internal void NotifyDatabaseAdding(Database db)
		{
			if (db.IsModelLoaded())
			{
				if (db.Model.IsRemoved)
				{
					throw new InvalidOperationException(TomSR.Exception_RemovedModelCannotBeAttached);
				}
				if (!base.IsInRefresh())
				{
					db.VerifyNoOtherModelInTransaction();
				}
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0006F870 File Offset: 0x0006DA70
		internal void NotifyDatabaseAdded(Database db)
		{
			if (db.IsModelLoaded() && db.Model.TxManager == null)
			{
				bool flag = base.IsInRefresh();
				bool flag2 = this.IsInTransaction && !flag;
				db.Model.CreateTxManager(flag2, flag);
				if (flag2)
				{
					base.CurrentTransaction.ModifiedDatabase = db;
				}
			}
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0006F8C8 File Offset: 0x0006DAC8
		protected internal override void CopyTo(MajorObject destination, bool forceBodyLoading)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Server server = destination as Server;
			if (server == null)
			{
				throw new ArgumentException(SR.Copy_InvalidDestination, "destination");
			}
			base.CopyTo(destination, forceBodyLoading);
			if (!base.IsLoaded && !forceBodyLoading)
			{
				return;
			}
			this.Databases.CopyTo(server.Databases, forceBodyLoading);
			this.Traces.CopyTo(server.Traces, forceBodyLoading);
			this.Roles.CopyTo(server.Roles, forceBodyLoading);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0006F948 File Offset: 0x0006DB48
		internal override void RefreshMajorChildren(RefreshType type)
		{
			foreach (object obj in this.Databases)
			{
				((Database)obj).Refresh(true, type);
			}
			foreach (object obj2 in this.Roles)
			{
				((Role)obj2).Refresh(true, type);
			}
			foreach (object obj3 in this.Traces)
			{
				((Trace)obj3).Refresh(true, type);
			}
			foreach (object obj4 in this.Assemblies)
			{
				((Assembly)obj4).Refresh(true, type);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0006FA78 File Offset: 0x0006DC78
		[XmlArray]
		[Browsable(false)]
		public DatabaseCollection Databases
		{
			get
			{
				return base.GetBody<Server.ServerBody>().Databases;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0006FA85 File Offset: 0x0006DC85
		[XmlIgnoreOnRead]
		internal AssemblyCollection Assemblies
		{
			get
			{
				return base.GetBody<Server.ServerBody>().Assemblies;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0006FA92 File Offset: 0x0006DC92
		[XmlArray]
		[Browsable(false)]
		public TraceCollection Traces
		{
			get
			{
				return base.GetBody<Server.ServerBody>().Traces;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0006FA9F File Offset: 0x0006DC9F
		[XmlArray]
		public RoleCollection Roles
		{
			get
			{
				return base.GetBody<Server.ServerBody>().Roles;
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0006FAAC File Offset: 0x0006DCAC
		internal override bool IsSyntacticallyValidID(string newValue, Type type, out string error)
		{
			return Utils.IsSyntacticallyValidID(newValue, type, out error);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0006FAB6 File Offset: 0x0006DCB6
		internal override bool IsValidName(string newValue, Type type, ModelType modelType, int compatibilityLevel, NamedComponentCollection namedComponentCollection, out string error)
		{
			return Utils.IsValidName(newValue, type, modelType, compatibilityLevel, namedComponentCollection, out error);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0006FAC6 File Offset: 0x0006DCC6
		internal override JaXmlSerializer GetSerializer(bool writeMajorChildren, bool writeReadOnlyProperties)
		{
			return SerializationUtil.Utility.GetSerializer(writeMajorChildren, writeReadOnlyProperties);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0006FAD4 File Offset: 0x0006DCD4
		internal override JaXmlSerializer GetSerializer(ObjectExpansion objectExpansion)
		{
			return SerializationUtil.Utility.GetSerializer(objectExpansion);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0006FAE1 File Offset: 0x0006DCE1
		internal override void SendBatch(ArrayList objectsToDelete, ArrayList objectToAlter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0006FAE8 File Offset: 0x0006DCE8
		internal override IEnumerable<Database> GetDatabases()
		{
			foreach (object obj in base.GetBody<Server.ServerBody>().Databases)
			{
				Database database = (Database)obj;
				yield return database;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040001A3 RID: 419
		private SessionTrace sessionTrace;

		// Token: 0x040001A4 RID: 420
		internal object txCleanupLock = new object();

		// Token: 0x020002EC RID: 748
		private sealed class ServerBody : Server.ServerBodyBase
		{
			// Token: 0x060023B4 RID: 9140 RVA: 0x000E29FE File Offset: 0x000E0BFE
			public ServerBody(Server owner)
				: base(owner)
			{
				this.Databases = new DatabaseCollection(owner);
				this.Roles = new RoleCollection(owner);
				this.Traces = new TraceCollection(owner);
				this.Assemblies = new AssemblyCollection(owner);
			}

			// Token: 0x04000ABD RID: 2749
			public DatabaseCollection Databases;

			// Token: 0x04000ABE RID: 2750
			public RoleCollection Roles;

			// Token: 0x04000ABF RID: 2751
			public TraceCollection Traces;

			// Token: 0x04000AC0 RID: 2752
			public AssemblyCollection Assemblies;
		}
	}
}
