using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000D9 RID: 217
	internal class ModificationCommandTreeGenerator
	{
		// Token: 0x060010A2 RID: 4258 RVA: 0x00025BF4 File Offset: 0x00023DF4
		public ModificationCommandTreeGenerator(DbModel model, DbConnection connection = null)
		{
			this._compiledModel = model.Compile();
			this._connection = connection;
			using (DbContext dbContext = this.CreateContext())
			{
				this._metadataWorkspace = ((IObjectContextAdapter)dbContext).ObjectContext.MetadataWorkspace;
			}
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00025C50 File Offset: 0x00023E50
		private DbContext CreateContext()
		{
			if (this._connection != null)
			{
				return new ModificationCommandTreeGenerator.TempDbContext(this._connection, this._compiledModel);
			}
			return new ModificationCommandTreeGenerator.TempDbContext(this._compiledModel);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00025C77 File Offset: 0x00023E77
		public IEnumerable<DbInsertCommandTree> GenerateAssociationInsert(string associationIdentity)
		{
			return this.GenerateAssociation<DbInsertCommandTree>(associationIdentity, EntityState.Added);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00025C81 File Offset: 0x00023E81
		public IEnumerable<DbDeleteCommandTree> GenerateAssociationDelete(string associationIdentity)
		{
			return this.GenerateAssociation<DbDeleteCommandTree>(associationIdentity, EntityState.Deleted);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00025C8B File Offset: 0x00023E8B
		private IEnumerable<TCommandTree> GenerateAssociation<TCommandTree>(string associationIdentity, EntityState state) where TCommandTree : DbCommandTree
		{
			AssociationType item = this._metadataWorkspace.GetItem<AssociationType>(associationIdentity, DataSpace.CSpace);
			using (DbContext context = this.CreateContext())
			{
				EntityType entityType = item.SourceEnd.GetEntityType();
				object obj = this.InstantiateAndAttachEntity(entityType, context);
				EntityType entityType2 = item.TargetEnd.GetEntityType();
				object obj2 = ((entityType.GetRootType() == entityType2.GetRootType()) ? obj : this.InstantiateAndAttachEntity(entityType2, context));
				((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.ChangeRelationshipState(obj, obj2, item.FullName, item.TargetEnd.Name, (state == EntityState.Deleted) ? state : EntityState.Added);
				using (CommandTracer commandTracer = new CommandTracer(context))
				{
					context.SaveChanges();
					foreach (DbCommandTree dbCommandTree in commandTracer.CommandTrees)
					{
						yield return (TCommandTree)((object)dbCommandTree);
					}
					IEnumerator<DbCommandTree> enumerator = null;
				}
				CommandTracer commandTracer = null;
			}
			DbContext context = null;
			yield break;
			yield break;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00025CAC File Offset: 0x00023EAC
		private object InstantiateAndAttachEntity(EntityType entityType, DbContext context)
		{
			Type clrType = entityType.GetClrType();
			DbSet dbSet = context.Set(clrType);
			object obj = this.InstantiateEntity(entityType, context, clrType, dbSet);
			ModificationCommandTreeGenerator.SetFakeReferenceKeyValues(obj, entityType);
			ModificationCommandTreeGenerator.SetFakeKeyValues(obj, entityType);
			dbSet.Attach(obj);
			return obj;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00025CEC File Offset: 0x00023EEC
		private object InstantiateEntity(EntityType entityType, DbContext context, Type clrType, DbSet set)
		{
			object obj;
			if (!clrType.IsAbstract())
			{
				obj = set.Create();
			}
			else
			{
				EntityType entityType2 = this._metadataWorkspace.GetItems<EntityType>(DataSpace.CSpace).First((EntityType et) => entityType.IsAncestorOf(et) && !et.Abstract);
				obj = context.Set(entityType2.GetClrType()).Create();
			}
			ModificationCommandTreeGenerator.InstantiateComplexProperties(obj, entityType.Properties);
			return obj;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00025D5A File Offset: 0x00023F5A
		public IEnumerable<DbModificationCommandTree> GenerateInsert(string entityIdentity)
		{
			return this.Generate(entityIdentity, EntityState.Added);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00025D64 File Offset: 0x00023F64
		public IEnumerable<DbModificationCommandTree> GenerateUpdate(string entityIdentity)
		{
			return this.Generate(entityIdentity, EntityState.Modified);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00025D6F File Offset: 0x00023F6F
		public IEnumerable<DbModificationCommandTree> GenerateDelete(string entityIdentity)
		{
			return this.Generate(entityIdentity, EntityState.Deleted);
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00025D79 File Offset: 0x00023F79
		private IEnumerable<DbModificationCommandTree> Generate(string entityIdentity, EntityState state)
		{
			EntityType item = this._metadataWorkspace.GetItem<EntityType>(entityIdentity, DataSpace.CSpace);
			using (DbContext context = this.CreateContext())
			{
				object obj = this.InstantiateAndAttachEntity(item, context);
				if (state != EntityState.Deleted)
				{
					context.Entry(obj).State = state;
				}
				this.ChangeRelationshipStates(context, item, obj, state);
				if (state == EntityState.Deleted)
				{
					context.Entry(obj).State = state;
				}
				this.HandleTableSplitting(context, item, obj, state);
				using (CommandTracer commandTracer = new CommandTracer(context))
				{
					((IObjectContextAdapter)context).ObjectContext.SaveChanges(SaveOptions.None);
					foreach (DbCommandTree dbCommandTree in commandTracer.CommandTrees)
					{
						yield return (DbModificationCommandTree)dbCommandTree;
					}
					IEnumerator<DbCommandTree> enumerator = null;
				}
				CommandTracer commandTracer = null;
			}
			DbContext context = null;
			yield break;
			yield break;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00025D98 File Offset: 0x00023F98
		private void ChangeRelationshipStates(DbContext context, EntityType entityType, object entity, EntityState state)
		{
			ObjectStateManager objectStateManager = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager;
			foreach (AssociationType associationType in from at in this._metadataWorkspace.GetItems<AssociationType>(DataSpace.CSpace)
				where !at.IsForeignKey && !at.IsManyToMany() && (at.SourceEnd.GetEntityType().IsAssignableFrom(entityType) || at.TargetEnd.GetEntityType().IsAssignableFrom(entityType))
				select at)
			{
				AssociationEndMember sourceEnd;
				AssociationEndMember targetEnd;
				if (!associationType.TryGuessPrincipalAndDependentEnds(out sourceEnd, out targetEnd))
				{
					sourceEnd = associationType.SourceEnd;
					targetEnd = associationType.TargetEnd;
				}
				if (targetEnd.GetEntityType().IsAssignableFrom(entityType))
				{
					EntityType entityType2 = sourceEnd.GetEntityType();
					Type clrType = entityType2.GetClrType();
					DbSet dbSet = context.Set(clrType);
					object obj = dbSet.Local.Cast<object>().SingleOrDefault<object>();
					if (obj == null || (entity == obj && state == EntityState.Added))
					{
						obj = this.InstantiateEntity(entityType2, context, clrType, dbSet);
						ModificationCommandTreeGenerator.SetFakeReferenceKeyValues(obj, entityType2);
						dbSet.Attach(obj);
					}
					if (sourceEnd.IsRequired() && state == EntityState.Modified)
					{
						object obj2 = this.InstantiateEntity(entityType2, context, clrType, dbSet);
						ModificationCommandTreeGenerator.SetFakeKeyValues(obj2, entityType2);
						dbSet.Attach(obj2);
						objectStateManager.ChangeRelationshipState(entity, obj2, associationType.FullName, sourceEnd.Name, EntityState.Deleted);
					}
					objectStateManager.ChangeRelationshipState(entity, obj, associationType.FullName, sourceEnd.Name, (state == EntityState.Deleted) ? state : EntityState.Added);
				}
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00025F1C File Offset: 0x0002411C
		private void HandleTableSplitting(DbContext context, EntityType entityType, object entity, EntityState state)
		{
			foreach (AssociationType associationType in from at in this._metadataWorkspace.GetItems<AssociationType>(DataSpace.CSpace)
				where at.IsForeignKey && at.IsRequiredToRequired() && !at.IsSelfReferencing() && (at.SourceEnd.GetEntityType().IsAssignableFrom(entityType) || at.TargetEnd.GetEntityType().IsAssignableFrom(entityType)) && this._metadataWorkspace.GetItems<AssociationType>(DataSpace.SSpace).All((AssociationType fk) => fk.Name != at.Name)
				select at)
			{
				AssociationEndMember sourceEnd;
				AssociationEndMember targetEnd;
				if (!associationType.TryGuessPrincipalAndDependentEnds(out sourceEnd, out targetEnd))
				{
					sourceEnd = associationType.SourceEnd;
					targetEnd = associationType.TargetEnd;
				}
				bool flag = false;
				EntityType entityType2;
				if (sourceEnd.GetEntityType().GetRootType() == entityType.GetRootType())
				{
					flag = true;
					entityType2 = targetEnd.GetEntityType();
				}
				else
				{
					entityType2 = sourceEnd.GetEntityType();
				}
				object obj = this.InstantiateAndAttachEntity(entityType2, context);
				if (!flag)
				{
					if (state == EntityState.Added)
					{
						context.Entry(entity).State = EntityState.Modified;
					}
					else if (state == EntityState.Deleted)
					{
						context.Entry(entity).State = EntityState.Unchanged;
					}
				}
				else if (state != EntityState.Modified)
				{
					context.Entry(obj).State = state;
				}
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0002602C File Offset: 0x0002422C
		private static void SetFakeReferenceKeyValues(object entity, EntityType entityType)
		{
			foreach (EdmProperty edmProperty in entityType.KeyProperties)
			{
				PropertyInfo clrPropertyInfo = edmProperty.GetClrPropertyInfo();
				object fakeReferenceKeyValue = ModificationCommandTreeGenerator.GetFakeReferenceKeyValue(edmProperty.UnderlyingPrimitiveType.PrimitiveTypeKind);
				if (fakeReferenceKeyValue != null)
				{
					clrPropertyInfo.GetPropertyInfoForSet().SetValue(entity, fakeReferenceKeyValue, null);
				}
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x000260A0 File Offset: 0x000242A0
		private static object GetFakeReferenceKeyValue(PrimitiveTypeKind primitiveTypeKind)
		{
			if (primitiveTypeKind != PrimitiveTypeKind.Binary)
			{
				switch (primitiveTypeKind)
				{
				case PrimitiveTypeKind.String:
					return "42";
				case PrimitiveTypeKind.Geometry:
					return DefaultSpatialServices.Instance.GeometryFromText("POINT (4 2)");
				case PrimitiveTypeKind.Geography:
					return DefaultSpatialServices.Instance.GeographyFromText("POINT (4 2)");
				}
				return null;
			}
			return new byte[0];
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00026100 File Offset: 0x00024300
		private static void SetFakeKeyValues(object entity, EntityType entityType)
		{
			foreach (EdmProperty edmProperty in entityType.KeyProperties)
			{
				PropertyInfo clrPropertyInfo = edmProperty.GetClrPropertyInfo();
				object fakeKeyValue = ModificationCommandTreeGenerator.GetFakeKeyValue(edmProperty.UnderlyingPrimitiveType.PrimitiveTypeKind);
				clrPropertyInfo.GetPropertyInfoForSet().SetValue(entity, fakeKeyValue, null);
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00026170 File Offset: 0x00024370
		private static object GetFakeKeyValue(PrimitiveTypeKind primitiveTypeKind)
		{
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				return new byte[] { 66 };
			case PrimitiveTypeKind.Boolean:
				return true;
			case PrimitiveTypeKind.Byte:
				return 66;
			case PrimitiveTypeKind.DateTime:
				return DateTime.Now;
			case PrimitiveTypeKind.Decimal:
				return 42m;
			case PrimitiveTypeKind.Double:
				return 42.0;
			case PrimitiveTypeKind.Guid:
				return Guid.NewGuid();
			case PrimitiveTypeKind.Single:
				return 42f;
			case PrimitiveTypeKind.SByte:
				return 42;
			case PrimitiveTypeKind.Int16:
				return 42;
			case PrimitiveTypeKind.Int32:
				return 42;
			case PrimitiveTypeKind.Int64:
				return 42L;
			case PrimitiveTypeKind.String:
				return "42'";
			case PrimitiveTypeKind.Time:
				return TimeSpan.FromMilliseconds(42.0);
			case PrimitiveTypeKind.DateTimeOffset:
				return DateTimeOffset.Now;
			case PrimitiveTypeKind.Geometry:
				return DefaultSpatialServices.Instance.GeometryFromText("POINT (4 3)");
			case PrimitiveTypeKind.Geography:
				return DefaultSpatialServices.Instance.GeographyFromText("POINT (4 3)");
			default:
				return null;
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0002628C File Offset: 0x0002448C
		private static void InstantiateComplexProperties(object structuralObject, IEnumerable<EdmProperty> properties)
		{
			foreach (EdmProperty edmProperty in properties)
			{
				if (edmProperty.IsComplexType)
				{
					PropertyInfo clrPropertyInfo = edmProperty.GetClrPropertyInfo();
					object obj = Activator.CreateInstance(clrPropertyInfo.PropertyType);
					ModificationCommandTreeGenerator.InstantiateComplexProperties(obj, edmProperty.ComplexType.Properties);
					clrPropertyInfo.GetPropertyInfoForSet().SetValue(structuralObject, obj, null);
				}
			}
		}

		// Token: 0x040008AB RID: 2219
		private readonly DbCompiledModel _compiledModel;

		// Token: 0x040008AC RID: 2220
		private readonly DbConnection _connection;

		// Token: 0x040008AD RID: 2221
		private readonly MetadataWorkspace _metadataWorkspace;

		// Token: 0x020007AC RID: 1964
		private class TempDbContext : DbContext
		{
			// Token: 0x060057DC RID: 22492 RVA: 0x00137417 File Offset: 0x00135617
			public TempDbContext(DbCompiledModel model)
				: base(model)
			{
				this.InternalContext.InitializerDisabled = true;
			}

			// Token: 0x060057DD RID: 22493 RVA: 0x0013742C File Offset: 0x0013562C
			public TempDbContext(DbConnection connection, DbCompiledModel model)
				: base(connection, model, false)
			{
				this.InternalContext.InitializerDisabled = true;
			}
		}
	}
}
