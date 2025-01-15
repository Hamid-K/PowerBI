using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005D1 RID: 1489
	internal class TableChangeProcessor
	{
		// Token: 0x060047C5 RID: 18373 RVA: 0x000FE6AE File Offset: 0x000FC8AE
		internal TableChangeProcessor(EntitySet table)
		{
			this.m_table = table;
			this.m_keyOrdinals = TableChangeProcessor.InitializeKeyOrdinals(table);
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x000FE6C9 File Offset: 0x000FC8C9
		protected TableChangeProcessor()
		{
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060047C7 RID: 18375 RVA: 0x000FE6D1 File Offset: 0x000FC8D1
		internal EntitySet Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060047C8 RID: 18376 RVA: 0x000FE6D9 File Offset: 0x000FC8D9
		internal int[] KeyOrdinals
		{
			get
			{
				return this.m_keyOrdinals;
			}
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x000FE6E4 File Offset: 0x000FC8E4
		internal bool IsKeyProperty(int propertyOrdinal)
		{
			foreach (int num in this.m_keyOrdinals)
			{
				if (propertyOrdinal == num)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060047CA RID: 18378 RVA: 0x000FE714 File Offset: 0x000FC914
		private static int[] InitializeKeyOrdinals(EntitySet table)
		{
			EntityType elementType = table.ElementType;
			IList<EdmMember> keyMembers = elementType.KeyMembers;
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(elementType);
			int[] array = new int[keyMembers.Count];
			for (int i = 0; i < keyMembers.Count; i++)
			{
				EdmMember edmMember = keyMembers[i];
				array[i] = allStructuralMembers.IndexOf(edmMember);
			}
			return array;
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x000FE768 File Offset: 0x000FC968
		internal List<UpdateCommand> CompileCommands(ChangeNode changeNode, UpdateCompiler compiler)
		{
			Set<CompositeKey> set = new Set<CompositeKey>(compiler.m_translator.KeyComparer);
			Dictionary<CompositeKey, PropagatorResult> dictionary = this.ProcessKeys(compiler, changeNode.Deleted, set);
			Dictionary<CompositeKey, PropagatorResult> dictionary2 = this.ProcessKeys(compiler, changeNode.Inserted, set);
			List<UpdateCommand> list = new List<UpdateCommand>(dictionary.Count + dictionary2.Count);
			foreach (CompositeKey compositeKey in set)
			{
				PropagatorResult propagatorResult;
				bool flag = dictionary.TryGetValue(compositeKey, out propagatorResult);
				PropagatorResult propagatorResult2;
				bool flag2 = dictionary2.TryGetValue(compositeKey, out propagatorResult2);
				try
				{
					if (!flag)
					{
						list.Add(compiler.BuildInsertCommand(propagatorResult2, this));
					}
					else if (!flag2)
					{
						list.Add(compiler.BuildDeleteCommand(propagatorResult, this));
					}
					else
					{
						UpdateCommand updateCommand = compiler.BuildUpdateCommand(propagatorResult, propagatorResult2, this);
						if (updateCommand != null)
						{
							list.Add(updateCommand);
						}
					}
				}
				catch (Exception ex)
				{
					if (ex.RequiresContext())
					{
						List<IEntityStateEntry> list2 = new List<IEntityStateEntry>();
						if (propagatorResult != null)
						{
							list2.AddRange(SourceInterpreter.GetAllStateEntries(propagatorResult, compiler.m_translator, this.m_table));
						}
						if (propagatorResult2 != null)
						{
							list2.AddRange(SourceInterpreter.GetAllStateEntries(propagatorResult2, compiler.m_translator, this.m_table));
						}
						throw new UpdateException(Strings.Update_GeneralExecutionException, ex, list2.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
					}
					throw;
				}
			}
			return list;
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x000FE8CC File Offset: 0x000FCACC
		private Dictionary<CompositeKey, PropagatorResult> ProcessKeys(UpdateCompiler compiler, List<PropagatorResult> changes, Set<CompositeKey> keys)
		{
			Dictionary<CompositeKey, PropagatorResult> dictionary = new Dictionary<CompositeKey, PropagatorResult>(compiler.m_translator.KeyComparer);
			foreach (PropagatorResult propagatorResult in changes)
			{
				PropagatorResult propagatorResult2 = propagatorResult;
				CompositeKey compositeKey = new CompositeKey(this.GetKeyConstants(propagatorResult2));
				PropagatorResult propagatorResult3;
				if (dictionary.TryGetValue(compositeKey, out propagatorResult3))
				{
					this.DiagnoseKeyCollision(compiler, propagatorResult, compositeKey, propagatorResult3);
				}
				dictionary.Add(compositeKey, propagatorResult2);
				keys.Add(compositeKey);
			}
			return dictionary;
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x000FE960 File Offset: 0x000FCB60
		private void DiagnoseKeyCollision(UpdateCompiler compiler, PropagatorResult change, CompositeKey key, PropagatorResult other)
		{
			KeyManager keyManager = compiler.m_translator.KeyManager;
			CompositeKey compositeKey = new CompositeKey(this.GetKeyConstants(other));
			bool flag = true;
			int num = 0;
			while (flag && num < key.KeyComponents.Length)
			{
				int identifier = key.KeyComponents[num].Identifier;
				int identifier2 = compositeKey.KeyComponents[num].Identifier;
				if (!keyManager.GetPrincipals(identifier).Intersect(keyManager.GetPrincipals(identifier2)).Any<int>())
				{
					flag = false;
				}
				num++;
			}
			if (flag)
			{
				IEnumerable<IEntityStateEntry> enumerable = SourceInterpreter.GetAllStateEntries(change, compiler.m_translator, this.m_table).Concat(SourceInterpreter.GetAllStateEntries(other, compiler.m_translator, this.m_table));
				throw new UpdateException(Strings.Update_DuplicateKeys, null, enumerable.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
			}
			HashSet<IEntityStateEntry> hashSet = null;
			foreach (PropagatorResult propagatorResult in key.KeyComponents.Concat(compositeKey.KeyComponents))
			{
				HashSet<IEntityStateEntry> hashSet2 = new HashSet<IEntityStateEntry>();
				foreach (int num2 in keyManager.GetDependents(propagatorResult.Identifier))
				{
					PropagatorResult propagatorResult2;
					if (keyManager.TryGetIdentifierOwner(num2, out propagatorResult2) && propagatorResult2.StateEntry != null)
					{
						hashSet2.Add(propagatorResult2.StateEntry);
					}
				}
				if (hashSet == null)
				{
					hashSet = new HashSet<IEntityStateEntry>(hashSet2);
				}
				else
				{
					hashSet.IntersectWith(hashSet2);
				}
			}
			throw new UpdateException(Strings.Update_GeneralExecutionException, new ConstraintException(Strings.Update_ReferentialConstraintIntegrityViolation), hashSet.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
		}

		// Token: 0x060047CE RID: 18382 RVA: 0x000FEB1C File Offset: 0x000FCD1C
		private PropagatorResult[] GetKeyConstants(PropagatorResult row)
		{
			PropagatorResult[] array = new PropagatorResult[this.m_keyOrdinals.Length];
			for (int i = 0; i < this.m_keyOrdinals.Length; i++)
			{
				PropagatorResult memberValue = row.GetMemberValue(this.m_keyOrdinals[i]);
				array[i] = memberValue;
			}
			return array;
		}

		// Token: 0x04001988 RID: 6536
		private readonly EntitySet m_table;

		// Token: 0x04001989 RID: 6537
		private readonly int[] m_keyOrdinals;
	}
}
