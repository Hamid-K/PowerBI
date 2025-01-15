using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000514 RID: 1300
	internal class ObjectItemConventionAssemblyLoader : ObjectItemAssemblyLoader
	{
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06003FF7 RID: 16375 RVA: 0x000D55D4 File Offset: 0x000D37D4
		public new virtual MutableAssemblyCacheEntry CacheEntry
		{
			get
			{
				return (MutableAssemblyCacheEntry)base.CacheEntry;
			}
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x000D55E1 File Offset: 0x000D37E1
		internal ObjectItemConventionAssemblyLoader(Assembly assembly, ObjectItemLoadingSessionData sessionData)
			: base(assembly, new MutableAssemblyCacheEntry(), sessionData)
		{
			base.SessionData.RegisterForLevel1PostSessionProcessing(this);
			this._factory = new ObjectItemConventionAssemblyLoader.ConventionOSpaceTypeFactory(this);
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x000D5614 File Offset: 0x000D3814
		protected override void LoadTypesFromAssembly()
		{
			foreach (Type type in base.SourceAssembly.GetAccessibleTypes())
			{
				EdmType edmType;
				if (this.TryGetCSpaceTypeMatch(type, out edmType))
				{
					if (type.IsValueType() && !type.IsEnum())
					{
						base.SessionData.LoadMessageLogger.LogLoadMessage(Strings.Validator_OSpace_Convention_Struct(edmType.FullName, type.FullName), edmType);
					}
					else
					{
						EdmType edmType2 = this._factory.TryCreateType(type, edmType);
						if (edmType2 != null)
						{
							this.CacheEntry.TypesInAssembly.Add(edmType2);
							if (!base.SessionData.CspaceToOspace.ContainsKey(edmType))
							{
								base.SessionData.CspaceToOspace.Add(edmType, edmType2);
							}
							else
							{
								EdmType edmType3 = base.SessionData.CspaceToOspace[edmType];
								base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_Convention_AmbiguousClrType(edmType.Name, edmType3.ClrType.FullName, type.FullName)));
							}
						}
					}
				}
			}
			if (base.SessionData.TypesInLoading.Count == 0)
			{
				base.SessionData.ObjectItemAssemblyLoaderFactory = null;
			}
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x000D5758 File Offset: 0x000D3958
		protected override void AddToAssembliesLoaded()
		{
			base.SessionData.AssembliesLoaded.Add(base.SourceAssembly, this.CacheEntry);
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x000D5778 File Offset: 0x000D3978
		private bool TryGetCSpaceTypeMatch(Type type, out EdmType cspaceType)
		{
			KeyValuePair<EdmType, int> keyValuePair;
			if (base.SessionData.ConventionCSpaceTypeNames.TryGetValue(type.Name, out keyValuePair))
			{
				if (keyValuePair.Value == 1)
				{
					cspaceType = keyValuePair.Key;
					return true;
				}
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_Convention_MultipleTypesWithSameName(type.Name)));
			}
			cspaceType = null;
			return false;
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x000D57D8 File Offset: 0x000D39D8
		internal override void OnLevel1SessionProcessing()
		{
			this.CreateRelationships();
			foreach (Action action in this._referenceResolutions)
			{
				action();
			}
			base.OnLevel1SessionProcessing();
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x000D5834 File Offset: 0x000D3A34
		internal virtual void TrackClosure(Type type)
		{
			if (base.SourceAssembly != type.Assembly() && !this.CacheEntry.ClosureAssemblies.Contains(type.Assembly()) && (!type.IsGenericType() || (!EntityUtil.IsAnICollection(type) && !(type.GetGenericTypeDefinition() == typeof(EntityReference<>)) && !(type.GetGenericTypeDefinition() == typeof(Nullable<>)))))
			{
				this.CacheEntry.ClosureAssemblies.Add(type.Assembly());
			}
			if (type.IsGenericType())
			{
				foreach (Type type2 in type.GetGenericArguments())
				{
					this.TrackClosure(type2);
				}
			}
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x000D58E8 File Offset: 0x000D3AE8
		private void CreateRelationships()
		{
			if (base.SessionData.ConventionBasedRelationshipsAreLoaded)
			{
				return;
			}
			base.SessionData.ConventionBasedRelationshipsAreLoaded = true;
			this._factory.CreateRelationships(base.SessionData.EdmItemCollection);
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x000D591A File Offset: 0x000D3B1A
		internal static bool SessionContainsConventionParameters(ObjectItemLoadingSessionData sessionData)
		{
			return sessionData.EdmItemCollection != null;
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x000D5925 File Offset: 0x000D3B25
		internal static ObjectItemAssemblyLoader Create(Assembly assembly, ObjectItemLoadingSessionData sessionData)
		{
			if (!ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(assembly))
			{
				return new ObjectItemConventionAssemblyLoader(assembly, sessionData);
			}
			sessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_Convention_AttributeAssemblyReferenced(assembly.FullName)));
			return new ObjectItemNoOpAssemblyLoader(assembly, sessionData);
		}

		// Token: 0x0400164E RID: 5710
		private readonly List<Action> _referenceResolutions = new List<Action>();

		// Token: 0x0400164F RID: 5711
		private readonly ObjectItemConventionAssemblyLoader.ConventionOSpaceTypeFactory _factory;

		// Token: 0x02000B1C RID: 2844
		internal class ConventionOSpaceTypeFactory : OSpaceTypeFactory
		{
			// Token: 0x06006480 RID: 25728 RVA: 0x0015B3F4 File Offset: 0x001595F4
			public ConventionOSpaceTypeFactory(ObjectItemConventionAssemblyLoader loader)
			{
				this._loader = loader;
			}

			// Token: 0x170010ED RID: 4333
			// (get) Token: 0x06006481 RID: 25729 RVA: 0x0015B403 File Offset: 0x00159603
			public override List<Action> ReferenceResolutions
			{
				get
				{
					return this._loader._referenceResolutions;
				}
			}

			// Token: 0x06006482 RID: 25730 RVA: 0x0015B410 File Offset: 0x00159610
			public override void LogLoadMessage(string message, EdmType relatedType)
			{
				this._loader.SessionData.LoadMessageLogger.LogLoadMessage(message, relatedType);
			}

			// Token: 0x06006483 RID: 25731 RVA: 0x0015B42C File Offset: 0x0015962C
			public override void LogError(string errorMessage, EdmType relatedType)
			{
				string text = this._loader.SessionData.LoadMessageLogger.CreateErrorMessageWithTypeSpecificLoadLogs(errorMessage, relatedType);
				this._loader.SessionData.EdmItemErrors.Add(new EdmItemError(text));
			}

			// Token: 0x06006484 RID: 25732 RVA: 0x0015B46C File Offset: 0x0015966C
			public override void TrackClosure(Type type)
			{
				this._loader.TrackClosure(type);
			}

			// Token: 0x170010EE RID: 4334
			// (get) Token: 0x06006485 RID: 25733 RVA: 0x0015B47A File Offset: 0x0015967A
			public override Dictionary<EdmType, EdmType> CspaceToOspace
			{
				get
				{
					return this._loader.SessionData.CspaceToOspace;
				}
			}

			// Token: 0x170010EF RID: 4335
			// (get) Token: 0x06006486 RID: 25734 RVA: 0x0015B48C File Offset: 0x0015968C
			public override Dictionary<string, EdmType> LoadedTypes
			{
				get
				{
					return this._loader.SessionData.TypesInLoading;
				}
			}

			// Token: 0x06006487 RID: 25735 RVA: 0x0015B49E File Offset: 0x0015969E
			public override void AddToTypesInAssembly(EdmType type)
			{
				this._loader.CacheEntry.TypesInAssembly.Add(type);
			}

			// Token: 0x04002CD8 RID: 11480
			private readonly ObjectItemConventionAssemblyLoader _loader;
		}
	}
}
