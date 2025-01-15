using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Mashup.Libraries
{
	// Token: 0x020020D7 RID: 8407
	public sealed class MemoryLibraryProvider : ILibraryProvider, IDisposable
	{
		// Token: 0x0600CE01 RID: 52737 RVA: 0x0028F7F8 File Offset: 0x0028D9F8
		public MemoryLibraryProvider(string name, bool tryPreserveWorkingLibraries = true)
		{
			this.lockObject = new object();
			this.identifier = string.Format(CultureInfo.InvariantCulture, "Memory({0})", name);
			this.libraries = new Dictionary<string, ILibrary>();
			this.tryPreserveWorkingLibraries = tryPreserveWorkingLibraries;
		}

		// Token: 0x0600CE02 RID: 52738 RVA: 0x0028F834 File Offset: 0x0028DA34
		public MemoryLibraryProvider(string name, Dictionary<string, byte[]> libraries, bool tryPreserveWorkingLibraries = true)
			: this(name, tryPreserveWorkingLibraries)
		{
			foreach (KeyValuePair<string, byte[]> keyValuePair in libraries)
			{
				this.AddLibrary(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600CE03 RID: 52739 RVA: 0x0028F898 File Offset: 0x0028DA98
		public MemoryLibraryProvider(string name, byte[][] libraries, bool tryPreserveWorkingLibraries = true)
			: this(name, tryPreserveWorkingLibraries)
		{
			foreach (byte[] array in libraries)
			{
				this.AddLibrary(Utilities.CreateHash(array), array);
			}
		}

		// Token: 0x17003181 RID: 12673
		// (get) Token: 0x0600CE04 RID: 52740 RVA: 0x0028F8CF File Offset: 0x0028DACF
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17003182 RID: 12674
		// (get) Token: 0x0600CE05 RID: 52741 RVA: 0x00002139 File Offset: 0x00000339
		public bool CanPersistMetadata
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600CE06 RID: 52742 RVA: 0x0028F8D8 File Offset: 0x0028DAD8
		// (remove) Token: 0x0600CE07 RID: 52743 RVA: 0x0028F910 File Offset: 0x0028DB10
		public event EventHandler<LibraryChangedEventArgs> Changed;

		// Token: 0x0600CE08 RID: 52744 RVA: 0x0028F948 File Offset: 0x0028DB48
		public IEnumerable<ILibrary> GetLibraries()
		{
			object obj = this.lockObject;
			IEnumerable<ILibrary> enumerable;
			lock (obj)
			{
				enumerable = this.libraries.Values.ToArray<ILibrary>();
			}
			return enumerable;
		}

		// Token: 0x0600CE09 RID: 52745 RVA: 0x0028F994 File Offset: 0x0028DB94
		public bool TryGetLibrary(string identifier, out ILibrary library)
		{
			object obj = this.lockObject;
			bool flag2;
			lock (obj)
			{
				flag2 = this.libraries.TryGetValue(identifier, out library);
			}
			return flag2;
		}

		// Token: 0x0600CE0A RID: 52746 RVA: 0x0028F9E0 File Offset: 0x0028DBE0
		public bool TryAddLibrary(string identifier, byte[] contents, out Exception error)
		{
			MemoryLibrary memoryLibrary = new MemoryLibrary(this, identifier, contents, true);
			object obj = this.lockObject;
			bool flag2;
			lock (obj)
			{
				ILibrary library;
				if (this.libraries.TryGetValue(identifier, out library))
				{
					error = new ArgumentException("duplicate key");
					flag2 = false;
				}
				else
				{
					this.libraries.Add(identifier, memoryLibrary);
					error = this.HandleChangeEvent(identifier, null, null);
					if (this.tryPreserveWorkingLibraries && error != null)
					{
						this.libraries.Remove(identifier);
						this.HandleChangeEvent(null, null, identifier);
					}
					flag2 = error == null;
				}
			}
			return flag2;
		}

		// Token: 0x0600CE0B RID: 52747 RVA: 0x0028FA8C File Offset: 0x0028DC8C
		public ILibrary AddLibrary(string identifier, byte[] contents)
		{
			object obj = this.lockObject;
			ILibrary library;
			lock (obj)
			{
				Exception ex;
				if (!this.TryAddLibrary(identifier, contents, out ex))
				{
					this.libraries.Remove(identifier);
					throw ex;
				}
				library = this.libraries[identifier];
			}
			return library;
		}

		// Token: 0x0600CE0C RID: 52748 RVA: 0x0028FAF0 File Offset: 0x0028DCF0
		public bool TryReplaceLibrary(string identifier, byte[] contents, out Exception error)
		{
			MemoryLibrary memoryLibrary = new MemoryLibrary(this, identifier, contents, true);
			object obj = this.lockObject;
			bool flag2;
			lock (obj)
			{
				ILibrary library;
				if (!this.libraries.TryGetValue(identifier, out library))
				{
					error = new ArgumentException("invalid key");
					flag2 = false;
				}
				else
				{
					this.libraries[identifier] = memoryLibrary;
					error = this.HandleChangeEvent(null, identifier, null);
					if (this.tryPreserveWorkingLibraries && error != null)
					{
						this.libraries[identifier] = library;
					}
					flag2 = error == null;
				}
			}
			return flag2;
		}

		// Token: 0x0600CE0D RID: 52749 RVA: 0x0028FB90 File Offset: 0x0028DD90
		public bool RemoveLibrary(string identifier)
		{
			object obj = this.lockObject;
			bool flag3;
			lock (obj)
			{
				bool flag2 = this.libraries.Remove(identifier);
				this.HandleChangeEvent(null, null, identifier);
				flag3 = flag2;
			}
			return flag3;
		}

		// Token: 0x0600CE0E RID: 52750 RVA: 0x0028FBE4 File Offset: 0x0028DDE4
		public void Clear()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				string[] array = this.libraries.Keys.ToArray<string>();
				this.libraries.Clear();
				EventHandler<LibraryChangedEventArgs> changed = this.Changed;
				if (changed != null)
				{
					LibraryChangedEventArgs libraryChangedEventArgs = new LibraryChangedEventArgs(null, null, array);
					changed(this, libraryChangedEventArgs);
				}
			}
		}

		// Token: 0x0600CE0F RID: 52751 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x0600CE10 RID: 52752 RVA: 0x0028FC58 File Offset: 0x0028DE58
		private Exception HandleChangeEvent(string added, string changed, string removed)
		{
			object obj;
			if (added == null)
			{
				obj = null;
			}
			else
			{
				(obj = new string[1])[0] = added;
			}
			object obj2;
			if (changed == null)
			{
				obj2 = null;
			}
			else
			{
				(obj2 = new string[1])[0] = changed;
			}
			object obj3;
			if (removed == null)
			{
				obj3 = null;
			}
			else
			{
				(obj3 = new string[1])[0] = removed;
			}
			LibraryChangedEventArgs libraryChangedEventArgs = new LibraryChangedEventArgs(obj, obj2, obj3);
			EventHandler<LibraryChangedEventArgs> changed2 = this.Changed;
			if (changed2 != null)
			{
				changed2(this, libraryChangedEventArgs);
			}
			Exception ex;
			if (libraryChangedEventArgs.Failures.TryGetValue(added ?? changed ?? removed, out ex))
			{
				return ex;
			}
			return null;
		}

		// Token: 0x04006827 RID: 26663
		private readonly object lockObject;

		// Token: 0x04006828 RID: 26664
		private readonly string identifier;

		// Token: 0x04006829 RID: 26665
		private readonly Dictionary<string, ILibrary> libraries;

		// Token: 0x0400682A RID: 26666
		private readonly bool tryPreserveWorkingLibraries;
	}
}
