using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000100 RID: 256
	public abstract class MetadataObject : ITxObject
	{
		// Token: 0x0600106F RID: 4207 RVA: 0x00078F13 File Offset: 0x00077113
		private protected MetadataObject()
		{
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001070 RID: 4208
		public abstract ObjectType ObjectType { get; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001071 RID: 4209
		// (set) Token: 0x06001072 RID: 4210
		public abstract MetadataObject Parent { get; internal set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x00078F1B File Offset: 0x0007711B
		internal MetadataObject LastParent
		{
			get
			{
				return this.Body.LastParent;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001074 RID: 4212
		internal abstract ObjectId ParentId { get; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00078F28 File Offset: 0x00077128
		public Model Model
		{
			get
			{
				if (this.ObjectType == ObjectType.Model)
				{
					return (Model)this;
				}
				if (this.IsRemoved)
				{
					return null;
				}
				if (this.model == null)
				{
					this.model = this.FindTopmost<Model>();
				}
				return this.model;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00078F5E File Offset: 0x0007715E
		public bool IsRemoved
		{
			get
			{
				return this.Body.IsRemoved;
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00078F6C File Offset: 0x0007716C
		internal static void UpdateMetadataObjectParent<TOwner, TObject>(ParentLink<TOwner, TObject> link, TObject parent, string requestingPath, CompatibilityRestrictionSet restrictions) where TOwner : MetadataObject where TObject : MetadataObject
		{
			if (link.Object != null)
			{
				CompatibilityMode compatibilityMode;
				if (link.Object.TryGetCurrentMode(out compatibilityMode))
				{
					link.Object.ResetCompatibilityRequirementImpl(compatibilityMode);
				}
				else
				{
					for (int i = 0; i < 3; i++)
					{
						link.Object.ResetCompatibilityRequirementImpl(CompatibilityRestrictionSet.GetModeByRestrictionIndex(i));
					}
				}
				if (parent == null)
				{
					Model model = link.Object.Model;
					if (model != null && model.TxManager != null)
					{
						link.Owner.Body.LastParent = link.Object;
					}
				}
			}
			link.Object = parent;
			if (parent != null)
			{
				MetadataObject.SetMetadataObjectParentRestrictions(link.Owner, parent, requestingPath, restrictions);
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00079040 File Offset: 0x00077240
		internal static TObject ResolveMetadataObjectParentById<TOwner, TObject>(ParentLink<TOwner, TObject> link, IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve, GetRestrictionsInfoByObjectType getRestrictionsInfo) where TOwner : MetadataObject where TObject : MetadataObject
		{
			if (link.ResolveById(objectMap, throwIfCantResolve))
			{
				string text;
				CompatibilityRestrictionSet compatibilityRestrictionSet;
				getRestrictionsInfo(link.Object.ObjectType, out text, out compatibilityRestrictionSet);
				MetadataObject.SetMetadataObjectParentRestrictions(link.Owner, link.Object, text, compatibilityRestrictionSet);
			}
			return link.Object;
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00079094 File Offset: 0x00077294
		internal static TObject ResolveMetadataObjectParentById<TOwner, TObject>(ParentLink<TOwner, TObject> link, IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve, string requestingPath, CompatibilityRestrictionSet restrictions) where TOwner : MetadataObject where TObject : MetadataObject
		{
			if (link.ResolveById(objectMap, throwIfCantResolve))
			{
				MetadataObject.SetMetadataObjectParentRestrictions(link.Owner, link.Object, requestingPath, restrictions);
			}
			return link.Object;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x000790C4 File Offset: 0x000772C4
		private static void SetMetadataObjectParentRestrictions(MetadataObject @object, MetadataObject parent, string requestingPath, CompatibilityRestrictionSet restrictions)
		{
			CompatibilityMode modeByRestrictionIndex;
			if (parent.TryGetCurrentMode(out modeByRestrictionIndex))
			{
				int num;
				string text;
				@object.GetCompatibilityRequirement(modeByRestrictionIndex, out num, out text);
				if (!string.IsNullOrEmpty(requestingPath))
				{
					text = MetadataObject.UpdateRequestingPath(text, requestingPath);
				}
				if (restrictions != null)
				{
					CompatibilityRestrictionSet.MergeLevelDemand(num, restrictions[modeByRestrictionIndex], out num);
				}
				parent.SetCompatibilityRequirement(new KeyValuePair<CompatibilityMode, KeyValuePair<int, string>>[]
				{
					new KeyValuePair<CompatibilityMode, KeyValuePair<int, string>>(modeByRestrictionIndex, new KeyValuePair<int, string>(num, string.Format("[{0}].{1}", parent.GetFormattedObjectPath(), text)))
				});
				return;
			}
			KeyValuePair<CompatibilityMode, KeyValuePair<int, string>>[] array = new KeyValuePair<CompatibilityMode, KeyValuePair<int, string>>[3];
			for (int i = 0; i < 3; i++)
			{
				modeByRestrictionIndex = CompatibilityRestrictionSet.GetModeByRestrictionIndex(i);
				int num2;
				string text2;
				@object.GetCompatibilityRequirement(modeByRestrictionIndex, out num2, out text2);
				if (!string.IsNullOrEmpty(requestingPath))
				{
					text2 = MetadataObject.UpdateRequestingPath(text2, requestingPath);
				}
				if (restrictions != null)
				{
					CompatibilityRestrictionSet.MergeLevelDemand(num2, restrictions[modeByRestrictionIndex], out num2);
				}
				array[i] = new KeyValuePair<CompatibilityMode, KeyValuePair<int, string>>(modeByRestrictionIndex, new KeyValuePair<int, string>(num2, string.Format("[{0}].{1}", parent.GetFormattedObjectPath(), text2)));
			}
			parent.SetCompatibilityRequirement(array);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x000791B8 File Offset: 0x000773B8
		internal int GetCompatibilityRequirementLevel(CompatibilityMode mode)
		{
			int num;
			string text;
			this.GetCompatibilityRequirement(mode, out num, out text);
			return num;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x000791D1 File Offset: 0x000773D1
		internal void GetCompatibilityRequirement(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			if (!this.Body.GetCompatibilityRequirement(mode, out requiredLevel, out requestingPath) && requiredLevel == -3)
			{
				this.ObtainCompatibilityRequirment(mode, out requiredLevel, out requestingPath);
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x000791F4 File Offset: 0x000773F4
		internal KeyValuePair<CompatibilityMode, Stack<string>>[] ValidateCompatibilityRequirement(CompatibilityRestrictionSet restrictions, string requestingPath)
		{
			CompatibilityMode modeByRestrictionIndex;
			KeyValuePair<CompatibilityMode, Stack<string>>[] array;
			if (this.TryGetCurrentMode(out modeByRestrictionIndex))
			{
				if (restrictions[modeByRestrictionIndex] == -2)
				{
					throw new CompatibilityViolationException(modeByRestrictionIndex, ClientHostingManager.MarkAsRestrictedInformation(MetadataObject.BuildRequestingPath(this, requestingPath), InfoRestrictionType.CCON));
				}
				array = new KeyValuePair<CompatibilityMode, Stack<string>>[1];
				Stack<string> stack = new Stack<string>();
				stack.Push(requestingPath);
				int num;
				if (!this.ValidateCompatibilityRequirementImpl(modeByRestrictionIndex, restrictions[modeByRestrictionIndex], stack, out num))
				{
					throw new CompatibilityViolationException(modeByRestrictionIndex, num, restrictions[modeByRestrictionIndex], ClientHostingManager.MarkAsRestrictedInformation(stack.Pop(), InfoRestrictionType.CCON));
				}
				array[0] = new KeyValuePair<CompatibilityMode, Stack<string>>(modeByRestrictionIndex, stack);
			}
			else
			{
				array = new KeyValuePair<CompatibilityMode, Stack<string>>[3];
				bool flag = false;
				for (int i = 0; i < 3; i++)
				{
					modeByRestrictionIndex = CompatibilityRestrictionSet.GetModeByRestrictionIndex(i);
					Stack<string> stack2 = new Stack<string>();
					stack2.Push(requestingPath);
					int num2;
					if (this.ValidateCompatibilityRequirementImpl(modeByRestrictionIndex, restrictions[modeByRestrictionIndex], stack2, out num2))
					{
						flag = true;
					}
					array[i] = new KeyValuePair<CompatibilityMode, Stack<string>>(modeByRestrictionIndex, stack2);
				}
				if (!flag)
				{
					throw new CompatibilityViolationException(ClientHostingManager.MarkAsRestrictedInformation(MetadataObject.BuildRequestingPath(this, requestingPath), InfoRestrictionType.CCON));
				}
			}
			return array;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x000792EC File Offset: 0x000774EC
		internal KeyValuePair<CompatibilityMode, Stack<string>>[] ValidateCompatibilityRequirement(MetadataObject child, string requestingPath, CompatibilityRestrictionSet restrictions)
		{
			CompatibilityMode modeByRestrictionIndex;
			KeyValuePair<CompatibilityMode, Stack<string>>[] array;
			if (this.TryGetCurrentMode(out modeByRestrictionIndex))
			{
				array = new KeyValuePair<CompatibilityMode, Stack<string>>[1];
				int num;
				string text;
				child.GetCompatibilityRequirement(modeByRestrictionIndex, out num, out text);
				if (!string.IsNullOrEmpty(requestingPath))
				{
					text = MetadataObject.UpdateRequestingPath(text, requestingPath);
				}
				if (restrictions != null)
				{
					CompatibilityRestrictionSet.MergeLevelDemand(num, restrictions[modeByRestrictionIndex], out num);
				}
				if (num == -2)
				{
					throw new CompatibilityViolationException(modeByRestrictionIndex, ClientHostingManager.MarkAsRestrictedInformation(MetadataObject.BuildRequestingPath(this, text), InfoRestrictionType.CCON));
				}
				Stack<string> stack = new Stack<string>();
				stack.Push(text);
				int num2;
				if (!this.ValidateCompatibilityRequirementImpl(modeByRestrictionIndex, num, stack, out num2))
				{
					throw new CompatibilityViolationException(modeByRestrictionIndex, num2, num, ClientHostingManager.MarkAsRestrictedInformation(stack.Pop(), InfoRestrictionType.CCON));
				}
				array[0] = new KeyValuePair<CompatibilityMode, Stack<string>>(modeByRestrictionIndex, stack);
			}
			else
			{
				array = new KeyValuePair<CompatibilityMode, Stack<string>>[3];
				bool flag = false;
				for (int i = 0; i < 3; i++)
				{
					modeByRestrictionIndex = CompatibilityRestrictionSet.GetModeByRestrictionIndex(i);
					int num3;
					string text2;
					child.GetCompatibilityRequirement(modeByRestrictionIndex, out num3, out text2);
					if (!string.IsNullOrEmpty(requestingPath))
					{
						text2 = MetadataObject.UpdateRequestingPath(text2, requestingPath);
					}
					if (restrictions != null)
					{
						CompatibilityRestrictionSet.MergeLevelDemand(num3, restrictions[modeByRestrictionIndex], out num3);
					}
					Stack<string> stack2 = new Stack<string>();
					stack2.Push(text2);
					int num4;
					if (this.ValidateCompatibilityRequirementImpl(modeByRestrictionIndex, num3, stack2, out num4))
					{
						flag = true;
					}
					array[i] = new KeyValuePair<CompatibilityMode, Stack<string>>(modeByRestrictionIndex, stack2);
				}
				if (!flag)
				{
					throw new CompatibilityViolationException(ClientHostingManager.MarkAsRestrictedInformation(MetadataObject.BuildRequestingPath(this, child.GetFormattedObjectPath()), InfoRestrictionType.CCON));
				}
			}
			return array;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00079440 File Offset: 0x00077640
		internal KeyValuePair<CompatibilityMode, Stack<string>>[] ValidateCompatibilityRequirement(MetadataObject source, CopyContext copyContext)
		{
			KeyValuePair<CompatibilityMode, Stack<string>>[] array;
			if (this.ObjectType == ObjectType.Model)
			{
				Database database = ((Model)this).Database;
				if (database != null)
				{
					if (database.CompatibilityMode != CompatibilityMode.Unknown)
					{
						int num;
						string text;
						source.GetCompatibilityRequirement(database.CompatibilityMode, out num, out text);
						if (num == -2)
						{
							throw new CompatibilityViolationException(database.CompatibilityMode, ClientHostingManager.MarkAsRestrictedInformation(text, InfoRestrictionType.CCON));
						}
						int compatibilityLevel = database.GetCompatibilityLevel();
						if (num > compatibilityLevel)
						{
							throw new CompatibilityViolationException(database.CompatibilityMode, compatibilityLevel, num, ClientHostingManager.MarkAsRestrictedInformation(text, InfoRestrictionType.CCON));
						}
						array = new KeyValuePair<CompatibilityMode, Stack<string>>[]
						{
							new KeyValuePair<CompatibilityMode, Stack<string>>(database.CompatibilityMode, new Stack<string>())
						};
					}
					else
					{
						array = new KeyValuePair<CompatibilityMode, Stack<string>>[3];
						bool flag = false;
						for (int i = 0; i < 3; i++)
						{
							CompatibilityMode modeByRestrictionIndex = CompatibilityRestrictionSet.GetModeByRestrictionIndex(i);
							int compatibilityRequirementLevel = source.GetCompatibilityRequirementLevel(modeByRestrictionIndex);
							if (compatibilityRequirementLevel != -2 && compatibilityRequirementLevel <= database.GetCompatibilityLevel())
							{
								flag = true;
							}
							array[i] = new KeyValuePair<CompatibilityMode, Stack<string>>(modeByRestrictionIndex, new Stack<string>());
						}
						if (!flag)
						{
							throw new CompatibilityViolationException(ClientHostingManager.MarkAsRestrictedInformation(source.GetFormattedObjectPath(), InfoRestrictionType.CCON));
						}
					}
				}
				else
				{
					array = new KeyValuePair<CompatibilityMode, Stack<string>>[3];
					for (int j = 0; j < 3; j++)
					{
						array[j] = new KeyValuePair<CompatibilityMode, Stack<string>>(CompatibilityRestrictionSet.GetModeByRestrictionIndex(j), new Stack<string>());
					}
				}
			}
			else if (this.Parent != null)
			{
				CompatibilityMode modeByRestrictionIndex2;
				if (this.Parent.TryGetCurrentMode(out modeByRestrictionIndex2))
				{
					int num2;
					string text2;
					source.GetCompatibilityRequirement(modeByRestrictionIndex2, out num2, out text2);
					if (num2 == -2)
					{
						throw new CompatibilityViolationException(modeByRestrictionIndex2, ClientHostingManager.MarkAsRestrictedInformation(text2, InfoRestrictionType.CCON));
					}
					Stack<string> stack = new Stack<string>();
					stack.Push(text2);
					int num3;
					if (!this.Parent.ValidateCompatibilityRequirementImpl(modeByRestrictionIndex2, num2, stack, out num3))
					{
						throw new CompatibilityViolationException(modeByRestrictionIndex2, num3, num2, ClientHostingManager.MarkAsRestrictedInformation(stack.Pop(), InfoRestrictionType.CCON));
					}
					array = new KeyValuePair<CompatibilityMode, Stack<string>>[]
					{
						new KeyValuePair<CompatibilityMode, Stack<string>>(modeByRestrictionIndex2, stack)
					};
				}
				else
				{
					array = new KeyValuePair<CompatibilityMode, Stack<string>>[3];
					bool flag2 = false;
					for (int k = 0; k < 3; k++)
					{
						modeByRestrictionIndex2 = CompatibilityRestrictionSet.GetModeByRestrictionIndex(k);
						int num4;
						string text3;
						source.GetCompatibilityRequirement(modeByRestrictionIndex2, out num4, out text3);
						Stack<string> stack2 = new Stack<string>();
						stack2.Push(text3);
						int num5;
						if (this.Parent.ValidateCompatibilityRequirementImpl(modeByRestrictionIndex2, num4, stack2, out num5))
						{
							flag2 = true;
						}
						array[k] = new KeyValuePair<CompatibilityMode, Stack<string>>(modeByRestrictionIndex2, stack2);
					}
					if (!flag2)
					{
						throw new CompatibilityViolationException(ClientHostingManager.MarkAsRestrictedInformation(source.GetFormattedObjectPath(), InfoRestrictionType.CCON));
					}
				}
			}
			else
			{
				array = new KeyValuePair<CompatibilityMode, Stack<string>>[3];
				for (int l = 0; l < 3; l++)
				{
					array[l] = new KeyValuePair<CompatibilityMode, Stack<string>>(CompatibilityRestrictionSet.GetModeByRestrictionIndex(l), new Stack<string>());
				}
			}
			return array;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x000796C8 File Offset: 0x000778C8
		internal void SetCompatibilityRequirement(CompatibilityRestrictionSet restrictions, KeyValuePair<CompatibilityMode, Stack<string>>[] requestingPaths)
		{
			for (int i = 0; i < requestingPaths.Length; i++)
			{
				this.SetCompatibilityRequirementImpl(requestingPaths[i].Key, restrictions[requestingPaths[i].Key], requestingPaths[i].Value, this.Body.Savepoint);
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00079720 File Offset: 0x00077920
		internal void SetCompatibilityRequirement(KeyValuePair<CompatibilityMode, KeyValuePair<int, string>>[] restrictions)
		{
			TxSavepoint txSavepoint = ((this.Body.Savepoint != null) ? this.Body.Savepoint.TxManager.CurrentSavepoint : null);
			for (int i = 0; i < restrictions.Length; i++)
			{
				this.SetCompatibilityRequirementImpl(restrictions[i].Key, restrictions[i].Value.Key, restrictions[i].Value.Value, txSavepoint);
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0007979C File Offset: 0x0007799C
		internal void SetCompatibilityRequirement(MetadataObject source, KeyValuePair<CompatibilityMode, Stack<string>>[] requestingPaths)
		{
			if (this.ObjectType == ObjectType.Model)
			{
				Database database = ((Model)this).Database;
				if (database != null && database.CompatibilityMode != CompatibilityMode.Unknown && database.GetCompatibilityLevel() < source.GetCompatibilityRequirementLevel(requestingPaths[0].Key))
				{
					throw new TomInternalException("The compatibility level is violating the associated db level; validation should have caought it before hand");
				}
			}
			else if (this.Parent != null)
			{
				for (int i = 0; i < requestingPaths.Length; i++)
				{
					this.Parent.SetCompatibilityRequirementImpl(requestingPaths[i].Key, source.GetCompatibilityRequirementLevel(requestingPaths[i].Key), requestingPaths[i].Value, this.Body.Savepoint);
				}
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00079848 File Offset: 0x00077A48
		internal void ResetCompatibilityRequirement()
		{
			if (this.Body.HasCompatibilityInfo(CompatibilityMode.Unknown))
			{
				CompatibilityMode compatibilityMode;
				if (this.TryGetCurrentMode(out compatibilityMode))
				{
					this.ResetCompatibilityRequirementImpl(compatibilityMode);
					return;
				}
				for (int i = 0; i < 3; i++)
				{
					this.ResetCompatibilityRequirementImpl(CompatibilityRestrictionSet.GetModeByRestrictionIndex(i));
				}
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0007988D File Offset: 0x00077A8D
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = -1;
			requestingPath = string.Empty;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0007989C File Offset: 0x00077A9C
		internal static string UpdateRequestingPath(string requestingPath, string rootPath)
		{
			if (string.IsNullOrEmpty(requestingPath))
			{
				return rootPath;
			}
			int num = requestingPath.IndexOf('.');
			if (num == -1)
			{
				return rootPath;
			}
			return string.Format("{0}{1}", rootPath, requestingPath.Substring(num));
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000798D4 File Offset: 0x00077AD4
		internal bool TryGetCurrentMode(out CompatibilityMode mode)
		{
			mode = ((this.Model != null && this.Model.Database != null) ? this.Model.Database.CompatibilityMode : CompatibilityMode.Unknown);
			return mode > CompatibilityMode.Unknown;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00079905 File Offset: 0x00077B05
		private static string BuildRequestingPath(MetadataObject obj, string requestingPath)
		{
			while (obj.Parent != null)
			{
				requestingPath = string.Format("[{0}].{1}", obj.GetFormattedObjectPath(), requestingPath);
				obj = obj.Parent;
			}
			return requestingPath;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00079930 File Offset: 0x00077B30
		private void ObtainCompatibilityRequirment(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			this.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel != -2)
			{
				foreach (MetadataObject metadataObject in this.GetChildren(false))
				{
					int num;
					string text;
					metadataObject.GetCompatibilityRequirement(mode, out num, out text);
					if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
					{
						requiredLevel = num;
						requestingPath = text;
					}
					if (num == -2)
					{
						break;
					}
				}
			}
			this.Body.UpdateCompatibilityRequirement(mode, requiredLevel, requestingPath);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x000799B8 File Offset: 0x00077BB8
		private bool ValidateCompatibilityRequirementImpl(CompatibilityMode mode, int compatLevelRequest, Stack<string> requestingPaths, out int supportedCompatLevel)
		{
			supportedCompatLevel = this.GetCompatibilityRequirementLevel(mode);
			if (CompatibilityRestrictionSet.CompareLevel(compatLevelRequest, supportedCompatLevel) == RestrictionsComapreResult.MoreRestrictive)
			{
				if (this.ObjectType == ObjectType.Model)
				{
					if (((Model)this).Database != null)
					{
						supportedCompatLevel = ((Model)this).Database.GetCompatibilityLevel();
						return compatLevelRequest != -2 && compatLevelRequest <= supportedCompatLevel;
					}
				}
				else if (this.Parent != null)
				{
					string text = string.Format("[{0}].{1}", this.Parent.GetFormattedObjectPath(), requestingPaths.Peek());
					requestingPaths.Push(text);
					if (!this.Parent.ValidateCompatibilityRequirementImpl(mode, compatLevelRequest, requestingPaths, out supportedCompatLevel))
					{
						return false;
					}
				}
			}
			return compatLevelRequest != -2 && supportedCompatLevel != -2;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00079A68 File Offset: 0x00077C68
		private void SetCompatibilityRequirementImpl(CompatibilityMode mode, int requiredLevel, Stack<string> requestingPaths, TxSavepoint savepoint)
		{
			int compatibilityRequirementLevel = this.GetCompatibilityRequirementLevel(mode);
			if (CompatibilityRestrictionSet.CompareLevel(requiredLevel, compatibilityRequirementLevel) == RestrictionsComapreResult.MoreRestrictive)
			{
				if (this.ObjectType == ObjectType.Model)
				{
					Database database = ((Model)this).Database;
					if (database != null && database.CompatibilityMode != CompatibilityMode.Unknown && database.GetCompatibilityLevel() < requiredLevel)
					{
						throw new TomInternalException("The compatibility level is violating the associated db level; validation should have caought it before hand");
					}
				}
				else if (this.Parent != null)
				{
					this.Parent.SetCompatibilityRequirementImpl(mode, requiredLevel, requestingPaths, savepoint);
				}
				if (savepoint != null && this.Body.Savepoint != savepoint)
				{
					this.CloneBody(savepoint);
				}
				this.Body.UpdateCompatibilityRequirement(mode, requiredLevel, requestingPaths.Pop());
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00079B04 File Offset: 0x00077D04
		private void SetCompatibilityRequirementImpl(CompatibilityMode mode, int requiredLevel, string requestingPath, TxSavepoint savepoint)
		{
			int compatibilityRequirementLevel = this.GetCompatibilityRequirementLevel(mode);
			if (CompatibilityRestrictionSet.CompareLevel(requiredLevel, compatibilityRequirementLevel) == RestrictionsComapreResult.MoreRestrictive)
			{
				if (this.ObjectType == ObjectType.Model)
				{
					Database database = ((Model)this).Database;
					if (database != null && database.CompatibilityMode != CompatibilityMode.Unknown && database.GetCompatibilityLevel() < requiredLevel)
					{
						throw new TomInternalException("The compatibility level is violating the associated db level; validation should have caought it before hand");
					}
				}
				else if (this.Parent != null)
				{
					this.Parent.SetCompatibilityRequirementImpl(mode, requiredLevel, string.Format("[{0}].{1}", this.Parent.GetFormattedObjectPath(), requestingPath), savepoint);
				}
				if (savepoint != null && this.Body.Savepoint != savepoint)
				{
					this.CloneBody(savepoint);
				}
				this.Body.UpdateCompatibilityRequirement(mode, requiredLevel, requestingPath);
			}
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00079BB1 File Offset: 0x00077DB1
		private void ResetCompatibilityRequirementImpl(CompatibilityMode mode)
		{
			if (this.Body.HasCompatibilityInfo(mode))
			{
				this.Body.UpdateCompatibilityRequirement(mode, -3, null);
				if (this.Parent != null)
				{
					this.Parent.ResetCompatibilityRequirementImpl(mode);
				}
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x00079BE4 File Offset: 0x00077DE4
		// (set) Token: 0x0600108E RID: 4238 RVA: 0x00079BF4 File Offset: 0x00077DF4
		internal ObjectId Id
		{
			get
			{
				return this.Body.Id;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.Body.Id, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Id", typeof(ObjectId), this.Body.Id, value);
					ObjectId id = this.Body.Id;
					this.Body.Id = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Id", typeof(ObjectId), id, value);
				}
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00079C78 File Offset: 0x00077E78
		internal IMetadataObjectCollection ParentCollection
		{
			get
			{
				if (this.Parent != null)
				{
					return this.GetParentCollection(this.Parent);
				}
				return null;
			}
		}

		// Token: 0x06001090 RID: 4240
		internal abstract IMetadataObjectCollection GetParentCollection(MetadataObject parent);

		// Token: 0x06001091 RID: 4241
		internal abstract string GetFormattedObjectPath();

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001092 RID: 4242
		// (set) Token: 0x06001093 RID: 4243
		internal abstract IMetadataObjectBody Body { get; set; }

		// Token: 0x06001094 RID: 4244
		internal abstract ITxObjectBody CreateBody();

		// Token: 0x06001095 RID: 4245
		internal abstract MetadataObject CreateObjectOfSameType();

		// Token: 0x06001096 RID: 4246 RVA: 0x00079C90 File Offset: 0x00077E90
		internal virtual IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames()
		{
			yield break;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00079C99 File Offset: 0x00077E99
		internal virtual void OnAfterBodyReverted()
		{
		}

		// Token: 0x06001098 RID: 4248
		internal abstract void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve);

		// Token: 0x06001099 RID: 4249
		internal abstract void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve);

		// Token: 0x0600109A RID: 4250 RVA: 0x00079C9C File Offset: 0x00077E9C
		internal string GetCustomizedPropertyName(string originalPropertyName)
		{
			foreach (CustomizedPropertyName customizedPropertyName in this.GetCustomizedPropertyNames())
			{
				if (customizedPropertyName.OriginalName == originalPropertyName)
				{
					return customizedPropertyName.CustomName;
				}
			}
			return originalPropertyName;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00079CFC File Offset: 0x00077EFC
		internal T FindTopmost<T>() where T : MetadataObject
		{
			T t = default(T);
			for (MetadataObject metadataObject = this; metadataObject != null; metadataObject = metadataObject.Parent)
			{
				if (metadataObject is T)
				{
					t = (T)((object)metadataObject);
				}
			}
			return t;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00079D30 File Offset: 0x00077F30
		internal MetadataObject FindRoot()
		{
			MetadataObject metadataObject = this.Parent;
			if (metadataObject == null)
			{
				return this;
			}
			while (metadataObject.Parent != null)
			{
				metadataObject = metadataObject.Parent;
			}
			return metadataObject;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00079D59 File Offset: 0x00077F59
		internal void MarkAsRemoved()
		{
			this.Body.IsRemoved = true;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00079D67 File Offset: 0x00077F67
		internal ObjectPath GetPath(IDictionary<ObjectId, MetadataObject> objectMap = null)
		{
			return new ObjectPath(MetadataObject.GetPathImpl(this, objectMap, false), true);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00079D77 File Offset: 0x00077F77
		private static IEnumerable<KeyValuePair<ObjectType, string>> GetPathImpl(MetadataObject @object, IDictionary<ObjectId, MetadataObject> objectMap, bool useOriginalName)
		{
			while (@object != null && @object.ObjectType != ObjectType.Model)
			{
				NamedMetadataObject namedMetadataObject = @object as NamedMetadataObject;
				IKeyedMetadataObject keyedMetadataObject = @object as IKeyedMetadataObject;
				if (namedMetadataObject != null)
				{
					string text;
					if (useOriginalName && namedMetadataObject.WasObjectRenamed(out text))
					{
						yield return new KeyValuePair<ObjectType, string>(@object.ObjectType, text);
					}
					else
					{
						yield return new KeyValuePair<ObjectType, string>(@object.ObjectType, namedMetadataObject.Name);
					}
				}
				else if (keyedMetadataObject != null)
				{
					yield return new KeyValuePair<ObjectType, string>(@object.ObjectType, keyedMetadataObject.LogicalPathElement);
				}
				else
				{
					yield return new KeyValuePair<ObjectType, string>(@object.ObjectType, null);
				}
				if (@object.Parent != null || objectMap == null || @object.ParentId == ObjectId.Null)
				{
					@object = @object.Parent;
				}
				else if (!objectMap.TryGetValue(@object.ParentId, out @object))
				{
					@object = null;
				}
			}
			yield break;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00079D98 File Offset: 0x00077F98
		internal IEqualityComparer<string> GetNamesComparer()
		{
			IEqualityComparer<string> namesComparerImpl = this.GetNamesComparerImpl(this.ObjectType != ObjectType.Model);
			if (namesComparerImpl != null)
			{
				return namesComparerImpl;
			}
			return StringComparer.OrdinalIgnoreCase;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00079DC4 File Offset: 0x00077FC4
		internal IEqualityComparer<string> GetNamesComparerImpl(bool checkOwningModel)
		{
			if (checkOwningModel)
			{
				Model model = this.Model;
				if (model != null)
				{
					return model.GetNamesComparerImpl(false);
				}
			}
			INamedMetadataObjectCollection namedMetadataObjectCollection = this.GetChildrenCollections(true).OfType<INamedMetadataObjectCollection>().FirstOrDefault<INamedMetadataObjectCollection>();
			if (namedMetadataObjectCollection != null)
			{
				return namedMetadataObjectCollection.GetNamesComparer();
			}
			foreach (MetadataObject metadataObject in this.GetDirectChildren(true))
			{
				IEqualityComparer<string> namesComparerImpl = metadataObject.GetNamesComparerImpl(false);
				if (namesComparerImpl != null)
				{
					return namesComparerImpl;
				}
			}
			return null;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00079E50 File Offset: 0x00078050
		internal IEnumerable<MetadataObject> GetChildren(bool isLogicalStructure)
		{
			IEnumerator<MetadataObject> enumerator2;
			foreach (IMetadataObjectCollection metadataObjectCollection in this.GetChildrenCollections(isLogicalStructure))
			{
				foreach (MetadataObject metadataObject in metadataObjectCollection.GetObjects())
				{
					yield return metadataObject;
				}
				enumerator2 = null;
			}
			IEnumerator<IMetadataObjectCollection> enumerator = null;
			foreach (MetadataObject metadataObject2 in this.GetDirectChildren(isLogicalStructure))
			{
				yield return metadataObject2;
			}
			enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00079E67 File Offset: 0x00078067
		internal virtual IEnumerable<MetadataObject> GetDirectChildren(bool isLogicalStructure)
		{
			yield break;
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00079E70 File Offset: 0x00078070
		internal virtual IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield break;
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00079E79 File Offset: 0x00078079
		internal IEnumerable<MetadataObject> GetAllDescendants()
		{
			return this.GetDescendantsImpl(false);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00079E82 File Offset: 0x00078082
		internal IEnumerable<MetadataObject> GetSelfAndAllDescendants()
		{
			return this.GetDescendantsImpl(true);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00079E8B File Offset: 0x0007808B
		internal IEnumerable<MetadataObject> GetAncestors()
		{
			return this.GetAncestorsImpl(false);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00079E94 File Offset: 0x00078094
		internal IEnumerable<MetadataObject> GetSelfAndAncestors()
		{
			return this.GetAncestorsImpl(true);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00079E9D File Offset: 0x0007809D
		private IEnumerable<MetadataObject> GetDescendantsImpl(bool includeSelf)
		{
			if (includeSelf)
			{
				yield return this;
			}
			foreach (MetadataObject metadataObject in this.GetChildren(false))
			{
				foreach (MetadataObject metadataObject2 in metadataObject.GetDescendantsImpl(true))
				{
					yield return metadataObject2;
				}
				IEnumerator<MetadataObject> enumerator2 = null;
			}
			IEnumerator<MetadataObject> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00079EB4 File Offset: 0x000780B4
		private IEnumerable<MetadataObject> GetAncestorsImpl(bool includeSelf)
		{
			if (includeSelf)
			{
				yield return this;
			}
			for (MetadataObject ancestor = this.Parent; ancestor != null; ancestor = ancestor.Parent)
			{
				yield return ancestor;
			}
			yield break;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00079ECB File Offset: 0x000780CB
		internal virtual void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00079ED0 File Offset: 0x000780D0
		internal virtual void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ID", out objectId))
			{
				this.Id = objectId;
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00079EF4 File Offset: 0x000780F4
		internal static void WriteObjectId(IPropertyWriter writer, WriteOptions options, string propNamePrefix, MetadataObject obj)
		{
			if (obj == null)
			{
				writer.WriteProperty<string>(options, propNamePrefix, PropertyHelper.ConvertStringToXml(ObjectId.Null.ToString()));
				return;
			}
			if (obj.Id == ObjectId.Model)
			{
				writer.WriteProperty<string>(options, propNamePrefix, PropertyHelper.ConvertStringToXml(obj.Id.ToString()));
				return;
			}
			if (!obj.Id.IsNull && (options & WriteOptions.WriteObjectPath) != WriteOptions.WriteObjectPath)
			{
				writer.WriteProperty<string>(options, propNamePrefix, PropertyHelper.ConvertStringToXml(obj.Id.ToString()));
				return;
			}
			obj.WriteObjectPath(writer, options, propNamePrefix);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00079F9C File Offset: 0x0007819C
		private void WriteObjectPath(IPropertyWriter writer, WriteOptions options, string prefix)
		{
			foreach (KeyValuePair<ObjectType, string> keyValuePair in new ObjectPath(MetadataObject.GetPathImpl(this, null, (options & WriteOptions.WriteOriginalNameInPath) == WriteOptions.WriteOriginalNameInPath), true))
			{
				ObjectType key = keyValuePair.Key;
				string value = keyValuePair.Value;
				if (!string.IsNullOrEmpty(value))
				{
					writer.WriteProperty<string>(options, string.Format("{0}.{1}", prefix, key.ToString()), PropertyHelper.ConvertStringToXml(value));
				}
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0007A034 File Offset: 0x00078234
		internal static void WriteMetadataObjectSchema(SerializationActivityContext context, ObjectType objectType, IMetadataSchemaWriter writer)
		{
			switch (objectType)
			{
			case ObjectType.Model:
				Model.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.DataSource:
				DataSource.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Table:
				Table.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Column:
				Column.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.AttributeHierarchy:
				AttributeHierarchy.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Partition:
				Partition.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Relationship:
				Relationship.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Measure:
				Measure.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Hierarchy:
				Hierarchy.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Level:
				Level.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Annotation:
				Annotation.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.KPI:
				KPI.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Culture:
				Culture.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.LinguisticMetadata:
				LinguisticMetadata.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Perspective:
				Perspective.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.PerspectiveTable:
				PerspectiveTable.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.PerspectiveColumn:
				PerspectiveColumn.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.PerspectiveHierarchy:
				PerspectiveHierarchy.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.PerspectiveMeasure:
				PerspectiveMeasure.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Role:
				ModelRole.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.RoleMembership:
				ModelRoleMember.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.TablePermission:
				TablePermission.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Variation:
				Variation.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Set:
				Set.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.PerspectiveSet:
				PerspectiveSet.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.ExtendedProperty:
				ExtendedProperty.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Expression:
				NamedExpression.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.ColumnPermission:
				ColumnPermission.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.DetailRowsDefinition:
				DetailRowsDefinition.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.RelatedColumnDetails:
				RelatedColumnDetails.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.GroupByColumn:
				GroupByColumn.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.CalculationGroup:
				CalculationGroup.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.CalculationItem:
				CalculationItem.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.AlternateOf:
				AlternateOf.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.RefreshPolicy:
				RefreshPolicy.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.FormatStringDefinition:
				FormatStringDefinition.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.QueryGroup:
				QueryGroup.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.AnalyticsAIMetadata:
				AnalyticsAIMetadata.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.ChangedProperty:
				ChangedProperty.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.ExcludedArtifact:
				ExcludedArtifact.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.DataCoverageDefinition:
				DataCoverageDefinition.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.CalculationExpression:
				CalculationGroupExpression.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Calendar:
				Calendar.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.TimeUnitColumnAssociation:
				TimeUnitColumnAssociation.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.Function:
				Function.WriteMetadataSchema(context, writer);
				return;
			case ObjectType.BindingInfo:
				BindingInfo.WriteMetadataSchema(context, writer);
				return;
			}
			throw TomInternalException.Create("Invalid object type - {0}!", new object[] { objectType });
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0007A2D8 File Offset: 0x000784D8
		internal static TMetadataObject CreateFromMetadataStream<TMetadataObject>(SerializationActivityContext context, ObjectType objectType, IMetadataReader reader) where TMetadataObject : MetadataObject
		{
			MetadataObject metadataObject;
			switch (objectType)
			{
			case ObjectType.Model:
				metadataObject = new Model();
				goto IL_031E;
			case ObjectType.DataSource:
				metadataObject = DataSource.CreateFromMetadataStream(context, reader);
				goto IL_031E;
			case ObjectType.Table:
				metadataObject = new Table();
				goto IL_031E;
			case ObjectType.Column:
				metadataObject = Column.CreateFromMetadataStream(context, reader);
				goto IL_031E;
			case ObjectType.AttributeHierarchy:
				metadataObject = new AttributeHierarchy();
				goto IL_031E;
			case ObjectType.Partition:
				metadataObject = new Partition();
				goto IL_031E;
			case ObjectType.Relationship:
				metadataObject = Relationship.CreateFromMetadataStream(context, reader);
				goto IL_031E;
			case ObjectType.Measure:
				metadataObject = new Measure();
				goto IL_031E;
			case ObjectType.Hierarchy:
				metadataObject = new Hierarchy();
				goto IL_031E;
			case ObjectType.Level:
				metadataObject = new Level();
				goto IL_031E;
			case ObjectType.Annotation:
				metadataObject = new Annotation();
				goto IL_031E;
			case ObjectType.KPI:
				metadataObject = new KPI();
				goto IL_031E;
			case ObjectType.Culture:
				metadataObject = new Culture();
				goto IL_031E;
			case ObjectType.ObjectTranslation:
				metadataObject = new ObjectTranslation();
				goto IL_031E;
			case ObjectType.LinguisticMetadata:
				metadataObject = new LinguisticMetadata();
				goto IL_031E;
			case ObjectType.Perspective:
				metadataObject = new Perspective();
				goto IL_031E;
			case ObjectType.PerspectiveTable:
				metadataObject = new PerspectiveTable();
				goto IL_031E;
			case ObjectType.PerspectiveColumn:
				metadataObject = new PerspectiveColumn();
				goto IL_031E;
			case ObjectType.PerspectiveHierarchy:
				metadataObject = new PerspectiveHierarchy();
				goto IL_031E;
			case ObjectType.PerspectiveMeasure:
				metadataObject = new PerspectiveMeasure();
				goto IL_031E;
			case ObjectType.Role:
				metadataObject = new ModelRole();
				goto IL_031E;
			case ObjectType.RoleMembership:
				metadataObject = ModelRoleMember.CreateFromMetadataStream(context, reader);
				goto IL_031E;
			case ObjectType.TablePermission:
				metadataObject = new TablePermission();
				goto IL_031E;
			case ObjectType.Variation:
				metadataObject = new Variation();
				goto IL_031E;
			case ObjectType.Set:
				metadataObject = new Set();
				goto IL_031E;
			case ObjectType.PerspectiveSet:
				metadataObject = new PerspectiveSet();
				goto IL_031E;
			case ObjectType.ExtendedProperty:
				metadataObject = ExtendedProperty.CreateFromMetadataStream(context, reader);
				goto IL_031E;
			case ObjectType.Expression:
				metadataObject = new NamedExpression();
				goto IL_031E;
			case ObjectType.ColumnPermission:
				metadataObject = new ColumnPermission();
				goto IL_031E;
			case ObjectType.DetailRowsDefinition:
				metadataObject = new DetailRowsDefinition();
				goto IL_031E;
			case ObjectType.RelatedColumnDetails:
				metadataObject = new RelatedColumnDetails();
				goto IL_031E;
			case ObjectType.GroupByColumn:
				metadataObject = new GroupByColumn();
				goto IL_031E;
			case ObjectType.CalculationGroup:
				metadataObject = new CalculationGroup();
				goto IL_031E;
			case ObjectType.CalculationItem:
				metadataObject = new CalculationItem();
				goto IL_031E;
			case ObjectType.AlternateOf:
				metadataObject = new AlternateOf();
				goto IL_031E;
			case ObjectType.RefreshPolicy:
				metadataObject = RefreshPolicy.CreateFromMetadataStream(context, reader);
				goto IL_031E;
			case ObjectType.FormatStringDefinition:
				metadataObject = new FormatStringDefinition();
				goto IL_031E;
			case ObjectType.QueryGroup:
				metadataObject = new QueryGroup();
				goto IL_031E;
			case ObjectType.AnalyticsAIMetadata:
				metadataObject = new AnalyticsAIMetadata();
				goto IL_031E;
			case ObjectType.ChangedProperty:
				metadataObject = new ChangedProperty();
				goto IL_031E;
			case ObjectType.ExcludedArtifact:
				metadataObject = new ExcludedArtifact();
				goto IL_031E;
			case ObjectType.DataCoverageDefinition:
				metadataObject = new DataCoverageDefinition();
				goto IL_031E;
			case ObjectType.CalculationExpression:
				metadataObject = new CalculationGroupExpression();
				goto IL_031E;
			case ObjectType.Calendar:
				metadataObject = new Calendar();
				goto IL_031E;
			case ObjectType.TimeUnitColumnAssociation:
				metadataObject = new TimeUnitColumnAssociation();
				goto IL_031E;
			case ObjectType.CalendarColumnReference:
				metadataObject = new CalendarColumnReference();
				goto IL_031E;
			case ObjectType.Function:
				metadataObject = new Function();
				goto IL_031E;
			case ObjectType.BindingInfo:
				metadataObject = BindingInfo.CreateFromMetadataStream(context, reader);
				goto IL_031E;
			}
			throw TomInternalException.Create("Invalid object type - {0}!", new object[] { objectType });
			IL_031E:
			metadataObject.LoadMetadata(context, reader);
			return (TMetadataObject)((object)metadataObject);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0007A611 File Offset: 0x00078811
		internal void LoadMetadata(SerializationActivityContext context, IMetadataReader reader)
		{
			this.OnDeserializeStart(context);
			this.ReadMetadataProperties(context, reader);
			this.OnDeserializeEnd(context);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0007A629 File Offset: 0x00078829
		internal void SaveMetadata(SerializationActivityContext context, IMetadataWriter writer)
		{
			this.OnSerializeStart(context);
			if (context.SerializationMode == MetadataSerializationMode.Xmla)
			{
				this.WriteMetadataBodyProperties(context, writer);
			}
			else
			{
				this.WriteMetadataTree(context, writer);
			}
			this.OnSerializeEnd(context);
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0007A654 File Offset: 0x00078854
		private protected virtual void OnDeserializeStart(SerializationActivityContext context)
		{
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0007A658 File Offset: 0x00078858
		private protected virtual void ReadMetadataProperties(SerializationActivityContext context, IMetadataReader reader)
		{
			while (reader.IsOnProperty())
			{
				UnexpectedPropertyClassification unexpectedPropertyClassification;
				if (!this.TryReadNextMetadataProperty(context, reader, out unexpectedPropertyClassification) && context.SerializationMode != MetadataSerializationMode.Xmla)
				{
					throw reader.CreateUnexpectedPropertyException(context, unexpectedPropertyClassification);
				}
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0007A68D File Offset: 0x0007888D
		private protected virtual bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			classification = UnexpectedPropertyClassification.Unclassified;
			if (reader.PropertyName == "ID")
			{
				this.Id = reader.ReadObjectIdProperty();
				return true;
			}
			return false;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0007A6B3 File Offset: 0x000788B3
		private protected virtual void OnDeserializeEnd(SerializationActivityContext context)
		{
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0007A6B5 File Offset: 0x000788B5
		private protected virtual void OnSerializeStart(SerializationActivityContext context)
		{
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0007A6B7 File Offset: 0x000788B7
		private protected virtual void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0007A6B9 File Offset: 0x000788B9
		private protected virtual void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0007A6BB File Offset: 0x000788BB
		private protected virtual void OnSerializeEnd(SerializationActivityContext context)
		{
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0007A6BD File Offset: 0x000788BD
		internal virtual void CopyFrom(MetadataObject other, CopyContext context)
		{
			context.OriginalToCloneObjectMap[other] = this;
			context.CopiedObjects.Add(this);
			if ((context.Flags & CopyFlags.IncludeObjectIds) == CopyFlags.IncludeObjectIds)
			{
				this.Id = other.Id;
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0007A6F0 File Offset: 0x000788F0
		internal void CopyFrom(MetadataObject other, CopyFlags flags)
		{
			CopyContext copyContext = new CopyContext(flags | CopyFlags.DontResolveCrossLinks, null);
			KeyValuePair<CompatibilityMode, Stack<string>>[] array = (((flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions) ? this.ValidateCompatibilityRequirement(other, copyContext) : null);
			this.CopyFrom(other, copyContext);
			if ((flags & CopyFlags.DontResolveCrossLinks) != CopyFlags.DontResolveCrossLinks)
			{
				this.TryResolveCrossLinksAfterSubtreeCopy(copyContext);
			}
			if ((flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions)
			{
				this.SetCompatibilityRequirement(other, array);
				return;
			}
			if (this.Parent != null)
			{
				this.Parent.ResetCompatibilityRequirement();
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0007A764 File Offset: 0x00078964
		internal T CloneInternal<T>() where T : MetadataObject
		{
			CopyContext copyContext = new CopyContext(CopyFlags.UserClone, null);
			T t = this.CloneInternal<T>(copyContext);
			t.Parent = null;
			t.TryResolveCrossLinksAfterSubtreeCopy(copyContext);
			return t;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0007A79C File Offset: 0x0007899C
		internal T CloneInternal<T>(CopyContext context) where T : MetadataObject
		{
			T t = (T)((object)this.CreateObjectOfSameType());
			t.CopyFrom(this, context);
			return t;
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0007A7B6 File Offset: 0x000789B6
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x0007A7BE File Offset: 0x000789BE
		ITxObjectBody ITxObject.Body
		{
			get
			{
				return this.Body;
			}
			set
			{
				this.Body = (IMetadataObjectBody)value;
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0007A7CC File Offset: 0x000789CC
		ITxObjectBody ITxObject.CreateBody()
		{
			return this.CreateBody();
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0007A7D4 File Offset: 0x000789D4
		void ITxObject.NotifyBodyReverted()
		{
			this.OnAfterBodyReverted();
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0007A7DC File Offset: 0x000789DC
		public ValidationResult Validate()
		{
			ValidationResult validationResult = new ValidationResult();
			foreach (MetadataObject metadataObject in this.GetSelfAndAllDescendants())
			{
				metadataObject.ValidateObjectImpl(validationResult, false);
			}
			return validationResult;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0007A830 File Offset: 0x00078A30
		internal virtual void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0007A834 File Offset: 0x00078A34
		internal bool TryResolveAllCrossLinksInTreeByObjectPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			using (IEnumerator<MetadataObject> enumerator = this.GetSelfAndAllDescendants().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.TryResolveCrossLinksByPath(linksFailedToResolve))
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0007A888 File Offset: 0x00078A88
		internal virtual bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			return true;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0007A88C File Offset: 0x00078A8C
		internal void TryResolveCrossLinksAfterSubtreeCopy(CopyContext copyContext)
		{
			foreach (MetadataObject metadataObject in copyContext.CopiedObjects)
			{
				metadataObject.TryResolveCrossLinksAfterCopy(copyContext);
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0007A8D8 File Offset: 0x00078AD8
		internal virtual void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0007A8DA File Offset: 0x00078ADA
		internal bool ContainsUnresolvedCrossLinks()
		{
			return this.GetSelfAndAllDescendants().Any((MetadataObject obj) => obj.ContainsUnresolvedCrossLinksImpl());
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0007A906 File Offset: 0x00078B06
		internal virtual bool ContainsUnresolvedCrossLinksImpl()
		{
			return false;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0007A909 File Offset: 0x00078B09
		internal virtual bool BuildIndirectNameCrossLinkPathIfNeeded()
		{
			return true;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0007A90C File Offset: 0x00078B0C
		internal virtual IEnumerable<MetadataObject> GetNameLinkedObjects(string objectName = null)
		{
			yield break;
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0007A915 File Offset: 0x00078B15
		internal void SetDirectChild(MetadataObject child)
		{
			this.SetDirectChildImpl(child);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0007A91E File Offset: 0x00078B1E
		internal void RemoveDirectChild(MetadataObject child)
		{
			this.RemoveDirectChildImpl(child);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0007A927 File Offset: 0x00078B27
		private protected virtual void SetDirectChildImpl(MetadataObject child)
		{
			throw TomInternalException.Create("Object of type {0} does not have a direct-child of type {1}", new object[] { this.ObjectType, child.ObjectType });
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0007A955 File Offset: 0x00078B55
		private protected virtual void RemoveDirectChildImpl(MetadataObject child)
		{
		}

		// Token: 0x060010D1 RID: 4305
		internal abstract void SerializeToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel);

		// Token: 0x060010D2 RID: 4306 RVA: 0x0007A958 File Offset: 0x00078B58
		internal JsonObject SerializeToNewJsonObject(SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			JsonObject jsonObject = new JsonObject();
			this.SerializeToJsonObject(jsonObject, options, mode, dbCompatibilityLevel);
			return jsonObject;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0007A978 File Offset: 0x00078B78
		internal void DeserializeFromJsonObject(JObject jsonObj, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			this.OnBeforeDeserialize(options);
			foreach (JProperty jproperty in jsonObj.Properties())
			{
				if (!this.ReadPropertyFromJson(jproperty, options, mode, dbCompatibilityLevel))
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(jproperty.Name), jproperty, null);
				}
			}
			this.OnAfterDeserialize(options);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0007A9EC File Offset: 0x00078BEC
		internal void DeserializeFromJsonObject(JToken rawJsonObj, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			rawJsonObj.VerifyTokenType(1);
			this.DeserializeFromJsonObject((JObject)rawJsonObj, options, mode, dbCompatibilityLevel);
		}

		// Token: 0x060010D5 RID: 4309
		internal abstract bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel);

		// Token: 0x060010D6 RID: 4310 RVA: 0x0007AA05 File Offset: 0x00078C05
		internal virtual void OnBeforeDeserialize(DeserializeOptions options)
		{
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0007AA07 File Offset: 0x00078C07
		internal virtual void OnAfterDeserialize(DeserializeOptions options)
		{
		}

		// Token: 0x04000251 RID: 593
		private Model model;
	}
}
