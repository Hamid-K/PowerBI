using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.AccessControlEntries;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Mashup.Engine1.Library.File
{
	// Token: 0x02000B6D RID: 2925
	internal static class FileHelper
	{
		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x060050CE RID: 20686 RVA: 0x0010EB0C File Offset: 0x0010CD0C
		private static Keys FileAttributeKeys
		{
			get
			{
				if (FileHelper.fileAttributeKeys == null)
				{
					FileHelper.fileAttributeKeys = Keys.New(new string[]
					{
						"Content Type", "Kind", "Size", "ReadOnly", "Hidden", "System", "Directory", "Archive", "Device", "Normal",
						"Temporary", "SparseFile", "ReparsePoint", "Compressed", "Offline", "NotContentIndexed", "Encrypted", "ChangeTime", "SymbolicLink", "MountPoint"
					});
				}
				return FileHelper.fileAttributeKeys;
			}
		}

		// Token: 0x17001937 RID: 6455
		// (get) Token: 0x060050CF RID: 20687 RVA: 0x0010EBE4 File Offset: 0x0010CDE4
		public static Keys FileEntryKeys
		{
			get
			{
				if (FileHelper.fileEntryKeys == null)
				{
					FileHelper.fileEntryKeys = Keys.New(new string[] { "Content", "Name", "Extension", "Date accessed", "Date modified", "Date created", "Attributes", "Folder Path" });
				}
				return FileHelper.fileEntryKeys;
			}
		}

		// Token: 0x17001938 RID: 6456
		// (get) Token: 0x060050D0 RID: 20688 RVA: 0x0010EC50 File Offset: 0x0010CE50
		private static IDictionary<string, TextValue> FileKinds
		{
			get
			{
				if (FileHelper.fileKinds == null)
				{
					FileHelper.fileKinds = new Dictionary<string, TextValue>(StringComparer.OrdinalIgnoreCase)
					{
						{
							"application/msaccess",
							TextValue.New("Access File")
						},
						{
							"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
							TextValue.New("Excel File")
						},
						{
							"application/vnd.ms-excel.sheet.binary.macroEnabled.12",
							TextValue.New("Excel File")
						},
						{
							"application/vnd.ms-excel.sheet.macroEnabled.12",
							TextValue.New("Excel File")
						},
						{
							"application/vnd.ms-excel",
							TextValue.New("Excel File")
						},
						{
							"application/javascript",
							TextValue.New("Javascript File")
						},
						{
							"application/json",
							TextValue.New("JSON File")
						},
						{
							"application/octet-stream",
							FileHelper.FileKind
						},
						{
							"application/xhtml+xml",
							TextValue.New("HTML File")
						},
						{
							"text/csv",
							TextValue.New("CSV File")
						},
						{
							"text/html",
							TextValue.New("HTML File")
						},
						{
							"text/javascript",
							TextValue.New("Javascript File")
						},
						{
							"text/x-json",
							TextValue.New("JSON File")
						},
						{
							"text/plain",
							FileHelper.TextFileKind
						},
						{
							"text/xml",
							TextValue.New("XML File")
						}
					};
				}
				return FileHelper.fileKinds;
			}
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x0010EDAC File Offset: 0x0010CFAC
		public static TableTypeValue FolderResultTypeValue(FileHelper.FolderOptions folderOptions)
		{
			bool flag = FileHelper.EnumerateFilesOnly(folderOptions);
			TableTypeValue tableTypeValue = TypeServices.ConvertToFolder(TableTypeValue.New(RecordTypeValue.New(flag ? FileHelper.FileEntryRecord.Concatenate(FileHelper.BinaryContentRecord).AsRecord : FileHelper.FileEntryRecord), FileHelper.EnumerateDeep(folderOptions) ? FileHelper.PathKey : FileHelper.NameKey), TextValue.New("Content"), TextValue.New("Name"), TextValue.New("Content"));
			if (flag)
			{
				return (TableTypeValue)NavigationTableServices.AddDataColumnIsLeafMetadata(tableTypeValue);
			}
			return tableTypeValue;
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x0010EE30 File Offset: 0x0010D030
		public static RecordValue CreateFileAttributesRecordValue(FileAttributes fileAttributes, Func<TextValue> delayedContentType, Func<Value> delayedKind, Value size, Func<Value> delayedChangeTime, Value symbolicLink, Value mountPoint)
		{
			return RecordValue.New(FileHelper.FileAttributeKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return delayedContentType();
				case 1:
					return delayedKind();
				case 2:
					return size;
				default:
					switch (index)
					{
					case 17:
						return delayedChangeTime();
					case 18:
						return symbolicLink;
					case 19:
						return mountPoint;
					default:
					{
						FileAttributes fileAttributes2 = (FileAttributes)Enum.Parse(typeof(FileAttributes), FileHelper.FileAttributeKeys[index]);
						return LogicalValue.New((fileAttributes & fileAttributes2) == fileAttributes2);
					}
					}
					break;
				}
			});
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x0010EE8D File Offset: 0x0010D08D
		public static bool EnumerateDeep(FileHelper.FolderOptions folderOptions)
		{
			return (folderOptions & FileHelper.FolderOptions.EnumerateDeep) == FileHelper.FolderOptions.EnumerateDeep;
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x0010EE95 File Offset: 0x0010D095
		public static bool EnumerateFiles(FileHelper.FolderOptions folderOptions)
		{
			return (folderOptions & FileHelper.FolderOptions.EnumerateFiles) == FileHelper.FolderOptions.EnumerateFiles;
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x0010EE9D File Offset: 0x0010D09D
		public static bool EnumerateFolders(FileHelper.FolderOptions folderOptions)
		{
			return (folderOptions & FileHelper.FolderOptions.EnumerateFolders) == FileHelper.FolderOptions.EnumerateFolders;
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x0010EEA5 File Offset: 0x0010D0A5
		public static bool EnumerateTables(FileHelper.FolderOptions folderOptions)
		{
			return (folderOptions & FileHelper.FolderOptions.EnumerateTables) == FileHelper.FolderOptions.EnumerateTables;
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x0010EEAD File Offset: 0x0010D0AD
		public static bool EnumeratingSubEntries(FileHelper.FolderOptions folderOptions)
		{
			return (folderOptions & FileHelper.FolderOptions.EnumeratingSubEntries) == FileHelper.FolderOptions.EnumeratingSubEntries;
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x0010EEB7 File Offset: 0x0010D0B7
		public static bool EnumerateFilesOnly(FileHelper.FolderOptions folderOptions)
		{
			return (folderOptions & FileHelper.FolderOptions.EnumerateTablesFoldersAndFiles) == FileHelper.FolderOptions.EnumerateFiles;
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x0010EEC0 File Offset: 0x0010D0C0
		public static string RemoveExtensionSeparator(string extension)
		{
			if (!extension.StartsWith(".", StringComparison.OrdinalIgnoreCase))
			{
				return extension;
			}
			return extension.Substring(".".Length);
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x0010EEE2 File Offset: 0x0010D0E2
		public static string AddExtensionSeparator(string extension)
		{
			if (!extension.StartsWith(".", StringComparison.OrdinalIgnoreCase))
			{
				return "." + extension;
			}
			return extension;
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x0010EF00 File Offset: 0x0010D100
		public static bool FileExists(string path)
		{
			bool flag;
			try
			{
				flag = File.Exists(path);
			}
			catch (Exception ex)
			{
				if (SafeExceptions.IsSafeException(ex))
				{
					throw FileErrors.HandleException(ex, TextValue.New(path));
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x0010EF40 File Offset: 0x0010D140
		public static string GetDirectoryName(string path)
		{
			string directoryName;
			try
			{
				directoryName = Path.GetDirectoryName(path);
			}
			catch (Exception ex)
			{
				if (SafeExceptions.IsSafeException(ex))
				{
					throw FileErrors.HandleException(ex, TextValue.New(path));
				}
				throw;
			}
			return directoryName;
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x0010EF80 File Offset: 0x0010D180
		public static string GetFullPath(string directory, string file)
		{
			string text;
			try
			{
				text = Path.Combine(directory, file);
			}
			catch (Exception ex)
			{
				if (SafeExceptions.IsSafeException(ex))
				{
					throw FileErrors.HandleException(ex, RecordValue.New(FileHelper.FolderPathAndNameKeys, new Value[] { ListValue.New(new Value[]
					{
						TextValue.New(directory),
						TextValue.New(file)
					}) }));
				}
				throw;
			}
			return text;
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x0010EFEC File Offset: 0x0010D1EC
		public static bool CanNormalizePath(string path)
		{
			string text;
			bool flag;
			return Resource.TryNormalizeFileUri(path, out text, out flag) && !flag;
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x0010F00C File Offset: 0x0010D20C
		public static bool IsSubPath(string path, string subPath)
		{
			string text;
			bool flag;
			string text2;
			return Resource.TryNormalizeFileUri(path, out text, out flag) && !flag && Resource.TryNormalizeFileUri(subPath, out text2, out flag) && !flag && subPath.StartsWith(path, StringComparison.Ordinal);
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x0010F041 File Offset: 0x0010D241
		public static bool ContainsPathSeparator(string name)
		{
			return name.IndexOfAny(FileHelper.DirectorySeparatorChars) != -1;
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x0010F054 File Offset: 0x0010D254
		public static TextValue GetFileExtension(string path)
		{
			TextValue textValue;
			try
			{
				string extension = Path.GetExtension(path);
				if (string.IsNullOrEmpty(extension))
				{
					textValue = TextValue.Empty;
				}
				else
				{
					textValue = TextValue.New(extension);
				}
			}
			catch (Exception ex)
			{
				if (SafeExceptions.IsSafeException(ex))
				{
					throw FileErrors.HandleException(ex, TextValue.New(path));
				}
				throw;
			}
			return textValue;
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x0010F0AC File Offset: 0x0010D2AC
		public static TextValue GetFileKind(string contentType)
		{
			if (string.IsNullOrEmpty(contentType))
			{
				return FileHelper.FileKind;
			}
			TextValue textValue;
			if (FileHelper.FileKinds.TryGetValue(contentType, out textValue))
			{
				return textValue;
			}
			if (contentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase))
			{
				return FileHelper.TextFileKind;
			}
			return FileHelper.FileKind;
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x0010F0F4 File Offset: 0x0010D2F4
		public static bool TryApplyExtension(string fileNamePattern, string text, out string newFileNamePattern)
		{
			string text2 = FileHelper.EnsureFilter(Path.GetExtension(fileNamePattern));
			if (!string.IsNullOrEmpty(text) && FileHelper.PatternContainsPattern(text2, text))
			{
				newFileNamePattern = Path.ChangeExtension(fileNamePattern, text);
				return true;
			}
			newFileNamePattern = null;
			return false;
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x0010F130 File Offset: 0x0010D330
		public static bool TryApplyFileName(string fileNamePattern, string text, out string newFileNamePattern)
		{
			string text2 = FileHelper.EnsureFilter(Path.GetFileNameWithoutExtension(fileNamePattern));
			string text3 = FileHelper.EnsureFilter(Path.GetExtension(fileNamePattern));
			string text4 = FileHelper.EnsureFilter(Path.GetFileNameWithoutExtension(text));
			string text5 = FileHelper.EnsureFilter(Path.GetExtension(text));
			if (FileHelper.PatternContainsPattern(text2, text4) && FileHelper.PatternContainsPattern(text3, text5))
			{
				newFileNamePattern = text;
				return true;
			}
			newFileNamePattern = null;
			return false;
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x0010F188 File Offset: 0x0010D388
		public static bool TryCombinePattern(string currentPattern, Value function, string text, out string newPattern)
		{
			if (currentPattern == "*")
			{
				if (function.Equals(Library.Text.Contains))
				{
					newPattern = "*" + text + "*";
					return true;
				}
				if (function.Equals(Library.Text.StartsWith))
				{
					newPattern = text + "*";
					return true;
				}
				if (function.Equals(Library.Text.EndsWith))
				{
					newPattern = "*" + text;
					return true;
				}
			}
			newPattern = null;
			return false;
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x0010F1FF File Offset: 0x0010D3FF
		public static void VerifyPath(string path)
		{
			FileHelper.GetDirectoryName(path);
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x0010F208 File Offset: 0x0010D408
		public static Func<IDisposable> VerifyPermissionAndGetImpersonationWrapper(IEngineHost host, IResource resource)
		{
			ResourceCredentialCollection resourceCredentialCollection;
			FileHelper.VerifyPermissionAndGetCredentials(host, resource, out resourceCredentialCollection);
			return resourceCredentialCollection[0].GetImpersonationWrapper(host, resource);
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x0010F22C File Offset: 0x0010D42C
		public static void VerifyPermissionAndGetCredentials(IEngineHost host, IResource resource, out ResourceCredentialCollection credentials)
		{
			credentials = HostResourcePermissionService.VerifyPermissionAndGetCredentials(host, resource, null);
			if (credentials.Count == 1 && FileHelper.VerifyCredential(credentials[0]))
			{
				return;
			}
			throw DataSourceException.NewInvalidCredentialsError(host, resource, null, null, null);
		}

		// Token: 0x060050E9 RID: 20713 RVA: 0x0010F25C File Offset: 0x0010D45C
		public static bool EndsWithDirectorySeparatorChar(string path)
		{
			return !string.IsNullOrEmpty(path) && path[path.Length - 1] == Path.DirectorySeparatorChar;
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x0010F280 File Offset: 0x0010D480
		public static void CreateDirectory(string directory)
		{
			try
			{
				Directory.CreateDirectory(directory);
			}
			catch (Exception ex)
			{
				if (SafeExceptions.IsSafeException(ex))
				{
					throw FileErrors.HandleException(ex, TextValue.New(directory));
				}
				throw;
			}
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x0010F2C0 File Offset: 0x0010D4C0
		public static void MoveDirectory(string sourcePath, string destinationPath)
		{
			try
			{
				if (!sourcePath.Equals(destinationPath))
				{
					Directory.Move(sourcePath, destinationPath);
				}
			}
			catch (Exception ex)
			{
				if (SafeExceptions.IsSafeException(ex))
				{
					throw FileErrors.HandleException(ex, TextValue.New(destinationPath));
				}
				throw;
			}
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x0010F308 File Offset: 0x0010D508
		public static void DeleteDirectory(string directory)
		{
			try
			{
				Directory.Delete(directory, true);
			}
			catch (DirectoryNotFoundException)
			{
			}
			catch (Exception ex)
			{
				if (SafeExceptions.IsSafeException(ex))
				{
					throw FileErrors.HandleException(ex, TextValue.New(directory));
				}
				throw;
			}
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x0010F358 File Offset: 0x0010D558
		public static void DeleteFile(string path)
		{
			try
			{
				File.Delete(path);
			}
			catch (DirectoryNotFoundException)
			{
			}
			catch (Exception ex)
			{
				if (SafeExceptions.IsSafeException(ex))
				{
					throw FileErrors.HandleException(ex, TextValue.New(path));
				}
				throw;
			}
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x0010F3A4 File Offset: 0x0010D5A4
		private static int GetKeyIndex(Keys keys, string keyName)
		{
			int num;
			if (!keys.TryGetKeyIndex(keyName, out num))
			{
				throw new InvalidOperationException();
			}
			return num;
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x0010F3C4 File Offset: 0x0010D5C4
		private static bool PatternContainsPattern(string container, string contained)
		{
			bool flag = false;
			bool flag2 = false;
			if (container.StartsWith("*", StringComparison.OrdinalIgnoreCase))
			{
				container = container.Substring("*".Length);
				flag = true;
			}
			if (container.EndsWith("*", StringComparison.OrdinalIgnoreCase))
			{
				container = container.Substring(0, container.Length - "*".Length);
				flag2 = true;
			}
			return string.Compare(container, contained, StringComparison.OrdinalIgnoreCase) == 0 || (flag && contained.EndsWith(container, StringComparison.OrdinalIgnoreCase)) || (flag2 && contained.StartsWith(container, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060050F0 RID: 20720 RVA: 0x0010F447 File Offset: 0x0010D647
		private static string EnsureFilter(string filter)
		{
			if (!string.IsNullOrEmpty(filter))
			{
				return filter;
			}
			return "*";
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x0010F458 File Offset: 0x0010D658
		public static IEnumerator<FileHelper.FileData> EnumerateFolderContents(string folderPath, string searchPattern, FileHelper.FolderOptions folderOptions, Func<IDisposable> impersonate, ITimeZone localTimeZone)
		{
			return new FileHelper.FolderContentsEnumerable(folderPath, searchPattern, folderOptions, impersonate, localTimeZone).GetEnumerator();
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x0010F46C File Offset: 0x0010D66C
		public static Value GetChangeTime(string fullName, Func<IDisposable> impersonate)
		{
			Value value;
			using (impersonate())
			{
				using (SafeFileHandle safeFileHandle = FileHelper.NativeMethods.CreateFile(fullName, FileHelper.NativeMethods.FILE_READ_ATTRIBUTES, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, FileHelper.NativeMethods.FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero))
				{
					IntPtr intPtr = IntPtr.Zero;
					try
					{
						if (safeFileHandle.IsInvalid)
						{
							throw FileErrors.WinIOError(Marshal.GetLastWin32Error(), fullName);
						}
						FileHelper.NativeMethods.IO_STATUS_BLOCK io_STATUS_BLOCK = default(FileHelper.NativeMethods.IO_STATUS_BLOCK);
						int num = Marshal.SizeOf(typeof(FileHelper.NativeMethods.FILE_BASIC_INFORMATION));
						intPtr = Marshal.AllocHGlobal(num);
						int num2 = FileHelper.NativeMethods.NtQueryInformationFile(safeFileHandle, ref io_STATUS_BLOCK, intPtr, (uint)num, FileHelper.NativeMethods.FILE_INFORMATION_CLASS.FileBasicInformation);
						if (num2 != 0)
						{
							throw FileErrors.WinIOError(FileHelper.NativeMethods.RtlNtStatusToDosError(num2), fullName);
						}
						value = DateTimeValue.New(DateTime.FromFileTime(((FileHelper.NativeMethods.FILE_BASIC_INFORMATION)Marshal.PtrToStructure(intPtr, typeof(FileHelper.NativeMethods.FILE_BASIC_INFORMATION))).ChangeTime));
					}
					catch (Exception ex)
					{
						if (SafeExceptions.IsSafeException(ex))
						{
							throw FileErrors.HandleException(ex, TextValue.New(fullName));
						}
						throw;
					}
					finally
					{
						if (intPtr != IntPtr.Zero)
						{
							Marshal.FreeHGlobal(intPtr);
						}
					}
				}
			}
			return value;
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x0010F59C File Offset: 0x0010D79C
		public static TableValue GetAccessControlEntries(FileHelper.AccessControlEntriesCache cache, string fullName, bool directory, FileSystemRights rights, Func<IDisposable> impersonate)
		{
			TableValue tableValue;
			using (impersonate())
			{
				if ((FileSystemRights.Read & rights) != rights)
				{
					throw new ArgumentException("Only read rights are supported.", "rights");
				}
				AuthorizationRuleCollection accessRules;
				try
				{
					accessRules = (directory ? new DirectoryInfo(fullName).GetAccessControl(AccessControlSections.Access) : new FileInfo(fullName).GetAccessControl(AccessControlSections.Access)).GetAccessRules(true, true, typeof(SecurityIdentifier));
				}
				catch (InvalidOperationException ex)
				{
					throw ValueException.NewDataSourceError<Message1>(Strings.File_CannotRetrieveAccessControlEntryTable(fullName), TextValue.New(fullName), ex);
				}
				catch (Exception ex2)
				{
					if (SafeExceptions.IsSafeException(ex2))
					{
						throw FileErrors.HandleException(ex2, TextValue.New(fullName));
					}
					throw;
				}
				HashSet<RecordValue> hashSet = new HashSet<RecordValue>();
				List<IValueReference> list = new List<IValueReference>();
				foreach (object obj in accessRules)
				{
					FileSystemAccessRule fileSystemAccessRule = (FileSystemAccessRule)obj;
					AccessControlType accessControlType = fileSystemAccessRule.AccessControlType;
					if (accessControlType != AccessControlType.Allow)
					{
						if (accessControlType == AccessControlType.Deny)
						{
							if ((fileSystemAccessRule.FileSystemRights & rights) == (FileSystemRights)0)
							{
								continue;
							}
							FunctionValue functionValue;
							if (cache.TryCreateIdentityCondition(fileSystemAccessRule.IdentityReference, out functionValue))
							{
								FileHelper.AddDenyEntry(hashSet, list, ActionModule.Action.Return, functionValue);
								continue;
							}
						}
						throw ValueException.NewDataSourceError<Message0>(Strings.File_CannotUnderstandAccessControlRule, RecordValue.New(new NamedValue[]
						{
							new NamedValue("FullName", TextValue.New(fullName)),
							new NamedValue("IdentityReferenceType", TextValue.New(fileSystemAccessRule.IdentityReference.GetType().FullName)),
							new NamedValue("IdentityReferenceValue", TextValue.New(fileSystemAccessRule.IdentityReference.Value))
						}), null);
					}
					FunctionValue functionValue2;
					if ((fileSystemAccessRule.FileSystemRights & rights) == rights && cache.TryCreateIdentityCondition(fileSystemAccessRule.IdentityReference, out functionValue2))
					{
						FileHelper.AddAllowEntry(hashSet, list, ActionModule.Action.Return, functionValue2);
					}
				}
				tableValue = ListValue.New(list).ToTable(AccessControlEntriesModule.AccessControlEntry.TableType);
			}
			return tableValue;
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x0010F7EC File Offset: 0x0010D9EC
		public static bool IsSymbolicLink(FileHelper.FileData fileData)
		{
			return (fileData.Attributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint && fileData.ReparseTag == FileHelper.NativeMethods.IO_REPARSE_TAG_SYMLINK;
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x0010F810 File Offset: 0x0010DA10
		public static bool IsMountPoint(FileHelper.FileData fileData)
		{
			return (fileData.Attributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint && fileData.ReparseTag == FileHelper.NativeMethods.IO_REPARSE_TAG_MOUNT_POINT;
		}

		// Token: 0x060050F6 RID: 20726 RVA: 0x0010F834 File Offset: 0x0010DA34
		public static Exception NewUnauthorizedAccessException(IEngineHost host, string path, FileHelper.FileData fileData)
		{
			string text = ((fileData == null) ? path : fileData.FullName);
			return DataSourceException.NewAccessAuthorizationError(host, Resource.New("Folder", text), text, null, null);
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x00002139 File Offset: 0x00000339
		public static bool ShouldConvertUnauthorizedAccess(string path, bool isMacSandboxUIEnabledFG)
		{
			return true;
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x0010F864 File Offset: 0x0010DA64
		private static void AddAllowEntry(HashSet<RecordValue> uniqueEntries, List<IValueReference> entries, FunctionValue action, FunctionValue condition)
		{
			RecordValue recordValue = RecordValue.New(AccessControlEntriesModule.AccessControlEntry.Type, new Value[]
			{
				AccessControlEntriesModule.AccessControlKind.Allow,
				action,
				condition
			});
			if (uniqueEntries.Add(recordValue))
			{
				entries.Add(recordValue);
			}
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x0010F8A4 File Offset: 0x0010DAA4
		private static void AddDenyEntry(HashSet<RecordValue> uniqueEntries, List<IValueReference> entries, FunctionValue action, FunctionValue condition)
		{
			RecordValue recordValue = RecordValue.New(AccessControlEntriesModule.AccessControlEntry.Type, new Value[]
			{
				AccessControlEntriesModule.AccessControlKind.Deny,
				action,
				condition
			});
			if (uniqueEntries.Add(recordValue))
			{
				entries.Add(recordValue);
			}
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x0010F8E4 File Offset: 0x0010DAE4
		private static bool TryGetIdentityKind(IdentityReference identity, out FileHelper.IdentityKind kind)
		{
			FileHelper.SafeLsaPolicyHandle safeLsaPolicyHandle = null;
			bool flag;
			try
			{
				safeLsaPolicyHandle = FileHelper.LsaOpenPolicy(FileHelper.NativeMethods.PolicyRights.POLICY_LOOKUP_NAMES);
				NTAccount ntaccount = identity as NTAccount;
				FileHelper.NativeMethods.SID_NAME_USE sid_NAME_USE;
				if (ntaccount != null && FileHelper.TryGetSidType(safeLsaPolicyHandle, ntaccount, out sid_NAME_USE))
				{
					flag = FileHelper.TryGetIdentityKind(sid_NAME_USE, out kind);
				}
				else
				{
					SecurityIdentifier securityIdentifier = identity as SecurityIdentifier;
					if (securityIdentifier != null && FileHelper.TryGetSidType(safeLsaPolicyHandle, securityIdentifier, out sid_NAME_USE))
					{
						flag = FileHelper.TryGetIdentityKind(sid_NAME_USE, out kind);
					}
					else
					{
						kind = FileHelper.IdentityKind.User;
						flag = false;
					}
				}
			}
			finally
			{
				FileHelper.DisposeNotNull<FileHelper.SafeLsaPolicyHandle>(ref safeLsaPolicyHandle);
			}
			return flag;
		}

		// Token: 0x060050FB RID: 20731 RVA: 0x0010F96C File Offset: 0x0010DB6C
		private static bool TryGetSidType(FileHelper.SafeLsaPolicyHandle lsaPolicyHandle, NTAccount account, out FileHelper.NativeMethods.SID_NAME_USE sidType)
		{
			FileHelper.SafeLsaMemoryHandle safeLsaMemoryHandle = null;
			FileHelper.SafeLsaMemoryHandle safeLsaMemoryHandle2 = null;
			bool flag;
			try
			{
				FileHelper.NativeMethods.LSA_UNICODE_STRING[] array = new FileHelper.NativeMethods.LSA_UNICODE_STRING[] { FileHelper.StringToLsaUnicodeString(account.Value) };
				FileHelper.VerifyLsaNtStatus(FileHelper.NativeMethods.LsaLookupNames(lsaPolicyHandle, array.Length, array, out safeLsaMemoryHandle, out safeLsaMemoryHandle2));
				FileHelper.NativeMethods.LSA_TRANSLATED_SID lsa_TRANSLATED_SID = (FileHelper.NativeMethods.LSA_TRANSLATED_SID)Marshal.PtrToStructure(new IntPtr((long)safeLsaMemoryHandle2.DangerousGetHandle()), typeof(FileHelper.NativeMethods.LSA_TRANSLATED_SID));
				sidType = lsa_TRANSLATED_SID.Use;
				flag = true;
			}
			catch (Win32Exception)
			{
				sidType = (FileHelper.NativeMethods.SID_NAME_USE)0;
				flag = false;
			}
			finally
			{
				FileHelper.DisposeNotNull<FileHelper.SafeLsaMemoryHandle>(ref safeLsaMemoryHandle);
				FileHelper.DisposeNotNull<FileHelper.SafeLsaMemoryHandle>(ref safeLsaMemoryHandle2);
			}
			return flag;
		}

		// Token: 0x060050FC RID: 20732 RVA: 0x0010FA14 File Offset: 0x0010DC14
		private static bool TryGetSidType(FileHelper.SafeLsaPolicyHandle lsaPolicyHandle, SecurityIdentifier identifier, out FileHelper.NativeMethods.SID_NAME_USE sidType)
		{
			FileHelper.SafeLsaMemoryHandle safeLsaMemoryHandle = null;
			FileHelper.SafeLsaMemoryHandle safeLsaMemoryHandle2 = null;
			GCHandle gchandle = default(GCHandle);
			bool flag;
			try
			{
				byte[] array = new byte[SecurityIdentifier.MaxBinaryLength];
				identifier.GetBinaryForm(array, 0);
				gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				IntPtr[] array2 = new IntPtr[] { gchandle.AddrOfPinnedObject() };
				FileHelper.VerifyLsaNtStatus(FileHelper.NativeMethods.LsaLookupSids(lsaPolicyHandle, array2.Length, array2, out safeLsaMemoryHandle, out safeLsaMemoryHandle2));
				FileHelper.NativeMethods.LSA_TRANSLATED_NAME lsa_TRANSLATED_NAME = (FileHelper.NativeMethods.LSA_TRANSLATED_NAME)Marshal.PtrToStructure(new IntPtr((long)safeLsaMemoryHandle2.DangerousGetHandle()), typeof(FileHelper.NativeMethods.LSA_TRANSLATED_NAME));
				sidType = lsa_TRANSLATED_NAME.Use;
				flag = true;
			}
			catch (Win32Exception)
			{
				sidType = (FileHelper.NativeMethods.SID_NAME_USE)0;
				flag = false;
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				FileHelper.DisposeNotNull<FileHelper.SafeLsaMemoryHandle>(ref safeLsaMemoryHandle);
				FileHelper.DisposeNotNull<FileHelper.SafeLsaMemoryHandle>(ref safeLsaMemoryHandle2);
			}
			return flag;
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x0010FAEC File Offset: 0x0010DCEC
		private static bool TryGetIdentityKind(FileHelper.NativeMethods.SID_NAME_USE sidType, out FileHelper.IdentityKind kind)
		{
			switch (sidType)
			{
			case FileHelper.NativeMethods.SID_NAME_USE.SidTypeUser:
				kind = FileHelper.IdentityKind.User;
				return true;
			case FileHelper.NativeMethods.SID_NAME_USE.SidTypeGroup:
			case FileHelper.NativeMethods.SID_NAME_USE.SidTypeAlias:
			case FileHelper.NativeMethods.SID_NAME_USE.SidTypeWellKnownGroup:
				kind = FileHelper.IdentityKind.Group;
				return true;
			}
			kind = FileHelper.IdentityKind.User;
			return false;
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x0010FB1C File Offset: 0x0010DD1C
		private static FileHelper.SafeLsaPolicyHandle LsaOpenPolicy(FileHelper.NativeMethods.PolicyRights rights)
		{
			FileHelper.SafeLsaPolicyHandle safeLsaPolicyHandle = null;
			FileHelper.SafeLsaPolicyHandle safeLsaPolicyHandle2;
			try
			{
				FileHelper.NativeMethods.LSA_OBJECT_ATTRIBUTES lsa_OBJECT_ATTRIBUTES = default(FileHelper.NativeMethods.LSA_OBJECT_ATTRIBUTES);
				lsa_OBJECT_ATTRIBUTES.Length = Marshal.SizeOf<FileHelper.NativeMethods.LSA_OBJECT_ATTRIBUTES>(lsa_OBJECT_ATTRIBUTES);
				FileHelper.VerifyLsaNtStatus(FileHelper.NativeMethods.LsaOpenPolicy(IntPtr.Zero, ref lsa_OBJECT_ATTRIBUTES, (int)rights, out safeLsaPolicyHandle));
				safeLsaPolicyHandle2 = safeLsaPolicyHandle;
			}
			catch
			{
				FileHelper.DisposeNotNull<FileHelper.SafeLsaPolicyHandle>(ref safeLsaPolicyHandle);
				throw;
			}
			return safeLsaPolicyHandle2;
		}

		// Token: 0x060050FF RID: 20735 RVA: 0x0010FB74 File Offset: 0x0010DD74
		private static FileHelper.NativeMethods.LSA_UNICODE_STRING StringToLsaUnicodeString(string s)
		{
			long num = (long)(s.Length * 2);
			if (num > 65535L)
			{
				throw new InvalidOperationException();
			}
			return new FileHelper.NativeMethods.LSA_UNICODE_STRING
			{
				Length = (ushort)num,
				MaximumLength = (ushort)num,
				Buffer = s
			};
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x0010FBBD File Offset: 0x0010DDBD
		private static void VerifyLsaNtStatus(FileHelper.NativeMethods.NTSTATUS status)
		{
			if (status != FileHelper.NativeMethods.NTSTATUS.SUCCESS)
			{
				throw new Win32Exception(FileHelper.NativeMethods.LsaNtStatusToWinError(status));
			}
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x0010FBCE File Offset: 0x0010DDCE
		private static void DisposeNotNull<T>(ref T disposable) where T : IDisposable
		{
			if (disposable != null)
			{
				disposable.Dispose();
			}
			disposable = default(T);
		}

		// Token: 0x06005102 RID: 20738 RVA: 0x0010FBF0 File Offset: 0x0010DDF0
		private static bool VerifyCredential(IResourceCredential credential)
		{
			return credential is WindowsCredential;
		}

		// Token: 0x04002B6C RID: 11116
		public const string FileEntryAttributesKey = "Attributes";

		// Token: 0x04002B6D RID: 11117
		public const string FileEntryAccessControlEntriesKey = "Access Control Entries";

		// Token: 0x04002B6E RID: 11118
		public const string FileEntryContentKey = "Content";

		// Token: 0x04002B6F RID: 11119
		public const string FileEntryDateAccessedKey = "Date accessed";

		// Token: 0x04002B70 RID: 11120
		public const string FileEntryDateCreatedKey = "Date created";

		// Token: 0x04002B71 RID: 11121
		public const string FileEntryDateModifiedKey = "Date modified";

		// Token: 0x04002B72 RID: 11122
		public const string FileEntryExtensionKey = "Extension";

		// Token: 0x04002B73 RID: 11123
		public const string FileEntryKindKey = "Kind";

		// Token: 0x04002B74 RID: 11124
		public const string FileEntryNameKey = "Name";

		// Token: 0x04002B75 RID: 11125
		public const string FileEntryFolderPathKey = "Folder Path";

		// Token: 0x04002B76 RID: 11126
		public const string FileEntrySizeKey = "Size";

		// Token: 0x04002B77 RID: 11127
		public const string FileEntryContentTypeKey = "Content Type";

		// Token: 0x04002B78 RID: 11128
		public const string FolderKindString = "Folder";

		// Token: 0x04002B79 RID: 11129
		public const string TableKindString = "Table";

		// Token: 0x04002B7A RID: 11130
		private const string FileKindString = "File";

		// Token: 0x04002B7B RID: 11131
		private const string AccessFileKindString = "Access File";

		// Token: 0x04002B7C RID: 11132
		private const string CSVFileKindString = "CSV File";

		// Token: 0x04002B7D RID: 11133
		private const string ExcelFileKindString = "Excel File";

		// Token: 0x04002B7E RID: 11134
		private const string HtmlFileKindString = "HTML File";

		// Token: 0x04002B7F RID: 11135
		private const string JavascriptFileKindString = "Javascript File";

		// Token: 0x04002B80 RID: 11136
		private const string JsonFileKindString = "JSON File";

		// Token: 0x04002B81 RID: 11137
		private const string TextFileKindString = "Text File";

		// Token: 0x04002B82 RID: 11138
		private const string XmlFileKindString = "XML File";

		// Token: 0x04002B83 RID: 11139
		public const string DefaultSearchPattern = "*";

		// Token: 0x04002B84 RID: 11140
		public const string DoubleDots = "..";

		// Token: 0x04002B85 RID: 11141
		private const string ExtensionSeparator = ".";

		// Token: 0x04002B86 RID: 11142
		public static readonly TextValue FileKind = TextValue.New("File");

		// Token: 0x04002B87 RID: 11143
		private static readonly TextValue TextFileKind = TextValue.New("Text File");

		// Token: 0x04002B88 RID: 11144
		public static readonly TextValue FolderKind = TextValue.New("Folder");

		// Token: 0x04002B89 RID: 11145
		public static readonly char[] DirectorySeparatorChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		// Token: 0x04002B8A RID: 11146
		public static readonly char[] SearchPatternWildcardChars = new char[] { '?', '*' };

		// Token: 0x04002B8B RID: 11147
		public static readonly Keys FolderPathAndNameKeys = Keys.New("Folder Path", "Name");

		// Token: 0x04002B8C RID: 11148
		public static readonly Keys TableEntryKeys = Keys.New("Content", "Name", "Folder Path");

		// Token: 0x04002B8D RID: 11149
		public static readonly RecordTypeValue TableEntryType = RecordTypeValue.New(RecordValue.New(FileHelper.TableEntryKeys, new Value[]
		{
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Table,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			})
		}));

		// Token: 0x04002B8E RID: 11150
		public static readonly TableTypeValue TableResultTypeWithNameKey = TableTypeValue.New(FileHelper.TableEntryType, new TableKey[]
		{
			new TableKey(new int[] { FileHelper.GetKeyIndex(FileHelper.FileEntryKeys, "Name") }, true)
		});

		// Token: 0x04002B8F RID: 11151
		private const char ExactlyOneCharWildcardChar = '?';

		// Token: 0x04002B90 RID: 11152
		private const char ZeroOrMoreCharsWildcardChar = '*';

		// Token: 0x04002B91 RID: 11153
		private const string SingleDot = ".";

		// Token: 0x04002B92 RID: 11154
		public const string SystemAttributeReadOnly = "ReadOnly";

		// Token: 0x04002B93 RID: 11155
		public const string SystemAttributeHidden = "Hidden";

		// Token: 0x04002B94 RID: 11156
		public const string SystemAttributeSystem = "System";

		// Token: 0x04002B95 RID: 11157
		public const string SystemAttributeDirectory = "Directory";

		// Token: 0x04002B96 RID: 11158
		public const string SystemAttributeArchive = "Archive";

		// Token: 0x04002B97 RID: 11159
		public const string SystemAttributeDevice = "Device";

		// Token: 0x04002B98 RID: 11160
		public const string SystemAttributeNormal = "Normal";

		// Token: 0x04002B99 RID: 11161
		public const string SystemAttributeTemporary = "Temporary";

		// Token: 0x04002B9A RID: 11162
		public const string SystemAttributeSparseFile = "SparseFile";

		// Token: 0x04002B9B RID: 11163
		public const string SystemAttributeReparsePoint = "ReparsePoint";

		// Token: 0x04002B9C RID: 11164
		public const string SystemAttributeCompressed = "Compressed";

		// Token: 0x04002B9D RID: 11165
		public const string SystemAttributeOffline = "Offline";

		// Token: 0x04002B9E RID: 11166
		public const string SystemAttributeNotContentIndexed = "NotContentIndexed";

		// Token: 0x04002B9F RID: 11167
		public const string SystemAttributeEncrypted = "Encrypted";

		// Token: 0x04002BA0 RID: 11168
		public const string ExtendedAttributeFileChangeTime = "ChangeTime";

		// Token: 0x04002BA1 RID: 11169
		public const string ExtendedAttributeSymLink = "SymbolicLink";

		// Token: 0x04002BA2 RID: 11170
		public const string ExtendedAttributeMountPoint = "MountPoint";

		// Token: 0x04002BA3 RID: 11171
		private static Keys fileAttributeKeys;

		// Token: 0x04002BA4 RID: 11172
		private static Keys fileEntryKeys;

		// Token: 0x04002BA5 RID: 11173
		private static IDictionary<string, TextValue> fileKinds;

		// Token: 0x04002BA6 RID: 11174
		private static readonly RecordValue FileEntryRecord = RecordValue.New(FileHelper.FileEntryKeys, new Value[]
		{
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Any,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				NullableTypeValue.DateTime,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				NullableTypeValue.DateTime,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				NullableTypeValue.DateTime,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				NullableTypeValue.Record,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			})
		});

		// Token: 0x04002BA7 RID: 11175
		private static readonly RecordValue BinaryContentRecord = RecordValue.New(Keys.New("Content"), new Value[] { RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
		{
			TypeValue.Binary,
			LogicalValue.False
		}) });

		// Token: 0x04002BA8 RID: 11176
		private static readonly TableKey[] NameKey = new TableKey[]
		{
			new TableKey(new int[] { FileHelper.GetKeyIndex(FileHelper.FileEntryKeys, "Name") }, true)
		};

		// Token: 0x04002BA9 RID: 11177
		private static readonly TableKey[] PathKey = new TableKey[]
		{
			new TableKey(new int[]
			{
				FileHelper.GetKeyIndex(FileHelper.FileEntryKeys, "Folder Path"),
				FileHelper.GetKeyIndex(FileHelper.FileEntryKeys, "Name")
			}, true)
		};

		// Token: 0x02000B6E RID: 2926
		public sealed class FileData
		{
			// Token: 0x06005104 RID: 20740 RVA: 0x0010FF20 File Offset: 0x0010E120
			public FileData(string folderPath, string name, FileAttributes attributes, DateTime creationTime, DateTime lastAccessTime, DateTime lastWriteTime, long length, uint reparseTag = 0U)
			{
				this.FullName = Path.Combine(folderPath, name);
				this.FolderPath = folderPath;
				this.Name = name;
				this.Attributes = attributes;
				this.CreationTime = creationTime;
				this.LastAccessTime = lastAccessTime;
				this.LastWriteTime = lastWriteTime;
				this.Length = length;
				this.ReparseTag = reparseTag;
			}

			// Token: 0x17001939 RID: 6457
			// (get) Token: 0x06005105 RID: 20741 RVA: 0x0010FF7D File Offset: 0x0010E17D
			// (set) Token: 0x06005106 RID: 20742 RVA: 0x0010FF85 File Offset: 0x0010E185
			public FileAttributes Attributes { get; private set; }

			// Token: 0x1700193A RID: 6458
			// (get) Token: 0x06005107 RID: 20743 RVA: 0x0010FF8E File Offset: 0x0010E18E
			// (set) Token: 0x06005108 RID: 20744 RVA: 0x0010FF96 File Offset: 0x0010E196
			public DateTime CreationTime { get; private set; }

			// Token: 0x1700193B RID: 6459
			// (get) Token: 0x06005109 RID: 20745 RVA: 0x0010FF9F File Offset: 0x0010E19F
			// (set) Token: 0x0600510A RID: 20746 RVA: 0x0010FFA7 File Offset: 0x0010E1A7
			public string FullName { get; private set; }

			// Token: 0x1700193C RID: 6460
			// (get) Token: 0x0600510B RID: 20747 RVA: 0x0010FFB0 File Offset: 0x0010E1B0
			// (set) Token: 0x0600510C RID: 20748 RVA: 0x0010FFB8 File Offset: 0x0010E1B8
			public string FolderPath { get; private set; }

			// Token: 0x1700193D RID: 6461
			// (get) Token: 0x0600510D RID: 20749 RVA: 0x0010FFC1 File Offset: 0x0010E1C1
			public bool IsFile
			{
				get
				{
					return (this.Attributes & FileAttributes.Directory) == (FileAttributes)0;
				}
			}

			// Token: 0x1700193E RID: 6462
			// (get) Token: 0x0600510E RID: 20750 RVA: 0x0010FFCF File Offset: 0x0010E1CF
			public bool IsFolder
			{
				get
				{
					return (this.Attributes & FileAttributes.Directory) == FileAttributes.Directory && this.Name != "." && this.Name != "..";
				}
			}

			// Token: 0x1700193F RID: 6463
			// (get) Token: 0x0600510F RID: 20751 RVA: 0x00110002 File Offset: 0x0010E202
			// (set) Token: 0x06005110 RID: 20752 RVA: 0x0011000A File Offset: 0x0010E20A
			public DateTime LastAccessTime { get; private set; }

			// Token: 0x17001940 RID: 6464
			// (get) Token: 0x06005111 RID: 20753 RVA: 0x00110013 File Offset: 0x0010E213
			// (set) Token: 0x06005112 RID: 20754 RVA: 0x0011001B File Offset: 0x0010E21B
			public DateTime LastWriteTime { get; private set; }

			// Token: 0x17001941 RID: 6465
			// (get) Token: 0x06005113 RID: 20755 RVA: 0x00110024 File Offset: 0x0010E224
			// (set) Token: 0x06005114 RID: 20756 RVA: 0x0011002C File Offset: 0x0010E22C
			public long Length { get; private set; }

			// Token: 0x17001942 RID: 6466
			// (get) Token: 0x06005115 RID: 20757 RVA: 0x00110035 File Offset: 0x0010E235
			// (set) Token: 0x06005116 RID: 20758 RVA: 0x0011003D File Offset: 0x0010E23D
			public string Name { get; private set; }

			// Token: 0x17001943 RID: 6467
			// (get) Token: 0x06005117 RID: 20759 RVA: 0x00110046 File Offset: 0x0010E246
			// (set) Token: 0x06005118 RID: 20760 RVA: 0x0011004E File Offset: 0x0010E24E
			public uint ReparseTag { get; private set; }
		}

		// Token: 0x02000B6F RID: 2927
		[Flags]
		public enum FolderOptions
		{
			// Token: 0x04002BB4 RID: 11188
			EnumerateDeep = 1,
			// Token: 0x04002BB5 RID: 11189
			EnumerateFiles = 2,
			// Token: 0x04002BB6 RID: 11190
			EnumerateFilesDeep = 3,
			// Token: 0x04002BB7 RID: 11191
			EnumerateFolders = 4,
			// Token: 0x04002BB8 RID: 11192
			EnumerateFoldersDeep = 5,
			// Token: 0x04002BB9 RID: 11193
			EnumerateFoldersAndFiles = 6,
			// Token: 0x04002BBA RID: 11194
			EnumerateFoldersAndFilesDeep = 7,
			// Token: 0x04002BBB RID: 11195
			EnumerateTables = 8,
			// Token: 0x04002BBC RID: 11196
			EnumerateTablesDeep = 9,
			// Token: 0x04002BBD RID: 11197
			EnumerateTablesAndFiles = 10,
			// Token: 0x04002BBE RID: 11198
			EnumerateTablesAndFilesDeep = 11,
			// Token: 0x04002BBF RID: 11199
			EnumerateTablesAndFolders = 12,
			// Token: 0x04002BC0 RID: 11200
			EnumerateTablesAndFoldersDeep = 13,
			// Token: 0x04002BC1 RID: 11201
			EnumerateTablesFoldersAndFiles = 14,
			// Token: 0x04002BC2 RID: 11202
			EnumerateTablesFoldersAndFilesDeep = 15,
			// Token: 0x04002BC3 RID: 11203
			EnumeratingSubEntries = 16,
			// Token: 0x04002BC4 RID: 11204
			EnumeratingSubfolders = 20
		}

		// Token: 0x02000B70 RID: 2928
		public enum IdentityKind
		{
			// Token: 0x04002BC6 RID: 11206
			User,
			// Token: 0x04002BC7 RID: 11207
			Group
		}

		// Token: 0x02000B71 RID: 2929
		private sealed class FolderContentsEnumerable : IEnumerable<FileHelper.FileData>, IEnumerable
		{
			// Token: 0x06005119 RID: 20761 RVA: 0x00110057 File Offset: 0x0010E257
			public FolderContentsEnumerable(string folderPath, string searchPattern, FileHelper.FolderOptions folderOptions, Func<IDisposable> impersonate, ITimeZone localTimeZone)
			{
				this.folderPath = folderPath;
				this.searchPattern = searchPattern;
				this.folderOptions = folderOptions;
				this.impersonate = impersonate;
				this.localTimeZone = localTimeZone;
			}

			// Token: 0x0600511A RID: 20762 RVA: 0x00110084 File Offset: 0x0010E284
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600511B RID: 20763 RVA: 0x0011008C File Offset: 0x0010E28C
			public IEnumerator<FileHelper.FileData> GetEnumerator()
			{
				return new FileHelper.FolderContentsEnumerable.FolderContentsEnumerator(this.folderPath, this.searchPattern, this.folderOptions, this.impersonate, this.localTimeZone);
			}

			// Token: 0x04002BC8 RID: 11208
			private readonly FileHelper.FolderOptions folderOptions;

			// Token: 0x04002BC9 RID: 11209
			private readonly string folderPath;

			// Token: 0x04002BCA RID: 11210
			private readonly string searchPattern;

			// Token: 0x04002BCB RID: 11211
			private readonly Func<IDisposable> impersonate;

			// Token: 0x04002BCC RID: 11212
			private readonly ITimeZone localTimeZone;

			// Token: 0x02000B72 RID: 2930
			private sealed class FolderContentsEnumerator : IEnumerator<FileHelper.FileData>, IDisposable, IEnumerator
			{
				// Token: 0x0600511C RID: 20764 RVA: 0x001100B4 File Offset: 0x0010E2B4
				~FolderContentsEnumerator()
				{
					this.Dispose(false);
				}

				// Token: 0x17001944 RID: 6468
				// (get) Token: 0x0600511D RID: 20765 RVA: 0x001100E4 File Offset: 0x0010E2E4
				public FileHelper.FileData Current
				{
					get
					{
						return this.current;
					}
				}

				// Token: 0x17001945 RID: 6469
				// (get) Token: 0x0600511E RID: 20766 RVA: 0x001100EC File Offset: 0x0010E2EC
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x0600511F RID: 20767 RVA: 0x001100F4 File Offset: 0x0010E2F4
				public FolderContentsEnumerator(string folderPath, string searchPattern, FileHelper.FolderOptions folderOptions, Func<IDisposable> impersonate, ITimeZone localTimeZone)
				{
					this.folderPath = folderPath;
					this.searchPattern = searchPattern;
					this.folderOptions = folderOptions;
					this.impersonate = impersonate;
					this.localTimeZone = localTimeZone;
				}

				// Token: 0x06005120 RID: 20768 RVA: 0x00110124 File Offset: 0x0010E324
				private void CloseFindHandle()
				{
					if (this.findHandle != null)
					{
						if (!this.findHandle.IsClosed)
						{
							using (this.impersonate())
							{
								this.findHandle.Close();
							}
						}
						this.findHandle = null;
					}
				}

				// Token: 0x06005121 RID: 20769 RVA: 0x00110180 File Offset: 0x0010E380
				private DateTime CreateDateTime(uint high, uint low)
				{
					return DateTime.FromFileTimeUtc(FileHelper.FolderContentsEnumerable.FolderContentsEnumerator.ToLong(high, low)).AdjustForTimeZone(this.localTimeZone);
				}

				// Token: 0x06005122 RID: 20770 RVA: 0x0011019C File Offset: 0x0010E39C
				private FileHelper.FileData CreateFileData(string folderPath, FileHelper.NativeMethods.WIN32_FIND_DATA findData)
				{
					return new FileHelper.FileData(folderPath, findData.cFileName, (FileAttributes)findData.dwFileAttributes, this.CreateDateTime(findData.ftCreationTimeHigh, findData.ftCreationTimeLow), this.CreateDateTime(findData.ftLastAccessTimeHigh, findData.ftLastAccessTimeLow), this.CreateDateTime(findData.ftLastWriteTimeHigh, findData.ftLastWriteTimeLow), FileHelper.FolderContentsEnumerable.FolderContentsEnumerator.ToLong(findData.nFileSizeHigh, findData.nFileSizeLow), findData.dwReserved0);
				}

				// Token: 0x06005123 RID: 20771 RVA: 0x00110208 File Offset: 0x0010E408
				private void Dispose(bool disposing)
				{
					this.CloseFindHandle();
					if (disposing)
					{
						this.current = null;
						this.DisposeFileEnumerator();
						this.DisposeFolderEnumerator();
					}
				}

				// Token: 0x06005124 RID: 20772 RVA: 0x00110228 File Offset: 0x0010E428
				private bool FindFirst(out FileHelper.FileData fileData)
				{
					this.findStarted = true;
					string text = Path.Combine(this.folderPath, this.searchPattern);
					if (!FileHelper.FolderContentsEnumerable.FolderContentsEnumerator.IsValidPathName(text))
					{
						throw FileErrors.WinIOError(123, text);
					}
					using (this.impersonate())
					{
						this.findHandle = FileHelper.NativeMethods.FindFirstFile(text, out this.findData);
					}
					if (!this.findHandle.IsInvalid)
					{
						fileData = this.CreateFileData(this.folderPath, this.findData);
						return true;
					}
					int lastWin32Error = Marshal.GetLastWin32Error();
					this.CloseFindHandle();
					if (lastWin32Error != 2 && lastWin32Error != 18 && (lastWin32Error != 5 || !FileHelper.EnumeratingSubEntries(this.folderOptions)))
					{
						throw FileErrors.WinIOError(lastWin32Error, text);
					}
					fileData = null;
					return false;
				}

				// Token: 0x06005125 RID: 20773 RVA: 0x001102F0 File Offset: 0x0010E4F0
				private bool FindNext(out FileHelper.FileData fileData)
				{
					if (this.findHandle == null)
					{
						fileData = null;
						return false;
					}
					bool flag;
					using (this.impersonate())
					{
						flag = FileHelper.NativeMethods.FindNextFile(this.findHandle, out this.findData);
					}
					if (flag)
					{
						fileData = this.CreateFileData(this.folderPath, this.findData);
						return true;
					}
					int lastWin32Error = Marshal.GetLastWin32Error();
					this.CloseFindHandle();
					if (lastWin32Error != 0 && lastWin32Error != 18)
					{
						throw FileErrors.WinIOError(lastWin32Error, this.folderPath);
					}
					fileData = null;
					return false;
				}

				// Token: 0x06005126 RID: 20774 RVA: 0x00110384 File Offset: 0x0010E584
				public void Reset()
				{
					this.current = null;
					this.findStarted = false;
					this.CloseFindHandle();
					this.DisposeFileEnumerator();
					this.DisposeFolderEnumerator();
				}

				// Token: 0x06005127 RID: 20775 RVA: 0x001103A6 File Offset: 0x0010E5A6
				public void Dispose()
				{
					this.Dispose(true);
					GC.SuppressFinalize(this);
				}

				// Token: 0x06005128 RID: 20776 RVA: 0x001103B5 File Offset: 0x0010E5B5
				private void DisposeFileEnumerator()
				{
					if (this.fileEnumerator != null)
					{
						this.fileEnumerator.Dispose();
						this.fileEnumerator = null;
					}
				}

				// Token: 0x06005129 RID: 20777 RVA: 0x001103D1 File Offset: 0x0010E5D1
				private void DisposeFolderEnumerator()
				{
					if (this.folderEnumerator != null)
					{
						this.folderEnumerator.Dispose();
						this.folderEnumerator = null;
					}
				}

				// Token: 0x0600512A RID: 20778 RVA: 0x001103ED File Offset: 0x0010E5ED
				private bool FindFile(out FileHelper.FileData fileData)
				{
					if (!this.findStarted)
					{
						return this.FindFirst(out fileData);
					}
					return this.FindNext(out fileData);
				}

				// Token: 0x0600512B RID: 20779 RVA: 0x00110408 File Offset: 0x0010E608
				private static bool IsValidPathName(string path)
				{
					string[] array = path.Split(FileHelper.DirectorySeparatorChars, StringSplitOptions.RemoveEmptyEntries);
					for (int i = 0; i < array.Length; i++)
					{
						if (string.Equals(array[i].Trim(), "..", StringComparison.OrdinalIgnoreCase))
						{
							return false;
						}
					}
					return true;
				}

				// Token: 0x0600512C RID: 20780 RVA: 0x00110448 File Offset: 0x0010E648
				public bool MoveNext()
				{
					FileHelper.FileData fileData;
					for (;;)
					{
						if (this.fileEnumerator == null)
						{
							if (this.MoveNextFile(out fileData))
							{
								break;
							}
						}
						else
						{
							if (this.fileEnumerator.MoveNext())
							{
								goto Block_2;
							}
							this.DisposeFileEnumerator();
						}
						if (!FileHelper.EnumerateDeep(this.folderOptions))
						{
							goto IL_00CB;
						}
						if (this.folderEnumerator == null)
						{
							this.folderEnumerator = new FileHelper.FolderContentsEnumerable.FolderContentsEnumerator(this.folderPath, "*", FileHelper.FolderOptions.EnumeratingSubfolders, this.impersonate, this.localTimeZone);
						}
						if (!this.folderEnumerator.MoveNext())
						{
							goto IL_00C2;
						}
						this.fileEnumerator = new FileHelper.FolderContentsEnumerable.FolderContentsEnumerator(this.folderEnumerator.Current.FullName, this.searchPattern, this.folderOptions | FileHelper.FolderOptions.EnumeratingSubEntries, this.impersonate, this.localTimeZone);
					}
					this.current = fileData;
					return true;
					Block_2:
					this.current = this.fileEnumerator.Current;
					return true;
					IL_00C2:
					this.current = null;
					return false;
					IL_00CB:
					this.current = null;
					return false;
				}

				// Token: 0x0600512D RID: 20781 RVA: 0x00110528 File Offset: 0x0010E728
				private bool MoveNextFile(out FileHelper.FileData fileData)
				{
					while (this.FindFile(out fileData))
					{
						if (FileHelper.EnumerateFiles(this.folderOptions) && fileData.IsFile)
						{
							return true;
						}
						if (FileHelper.EnumerateFolders(this.folderOptions) && fileData.IsFolder)
						{
							return true;
						}
					}
					fileData = null;
					return false;
				}

				// Token: 0x0600512E RID: 20782 RVA: 0x00110574 File Offset: 0x0010E774
				private static long ToLong(uint high, uint low)
				{
					return (long)(((ulong)high << 32) | (ulong)low);
				}

				// Token: 0x04002BCD RID: 11213
				private readonly FileHelper.FolderOptions folderOptions;

				// Token: 0x04002BCE RID: 11214
				private readonly string folderPath;

				// Token: 0x04002BCF RID: 11215
				private readonly string searchPattern;

				// Token: 0x04002BD0 RID: 11216
				private readonly Func<IDisposable> impersonate;

				// Token: 0x04002BD1 RID: 11217
				private readonly ITimeZone localTimeZone;

				// Token: 0x04002BD2 RID: 11218
				private FileHelper.FileData current;

				// Token: 0x04002BD3 RID: 11219
				private bool findStarted;

				// Token: 0x04002BD4 RID: 11220
				private FileHelper.FolderContentsEnumerable.FolderContentsEnumerator fileEnumerator;

				// Token: 0x04002BD5 RID: 11221
				private FileHelper.FolderContentsEnumerable.FolderContentsEnumerator folderEnumerator;

				// Token: 0x04002BD6 RID: 11222
				private FileHelper.NativeMethods.WIN32_FIND_DATA findData;

				// Token: 0x04002BD7 RID: 11223
				private FileHelper.SafeFindHandle findHandle;
			}
		}

		// Token: 0x02000B73 RID: 2931
		private class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x0600512F RID: 20783 RVA: 0x0000EDD3 File Offset: 0x0000CFD3
			public SafeFindHandle()
				: base(true)
			{
			}

			// Token: 0x06005130 RID: 20784 RVA: 0x0011057E File Offset: 0x0010E77E
			protected override bool ReleaseHandle()
			{
				return FileHelper.NativeMethods.FindClose(this.handle);
			}
		}

		// Token: 0x02000B74 RID: 2932
		private static class NativeMethods
		{
			// Token: 0x06005131 RID: 20785
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern FileHelper.SafeFindHandle FindFirstFile(string lpFileName, out FileHelper.NativeMethods.WIN32_FIND_DATA lpFindFileData);

			// Token: 0x06005132 RID: 20786
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool FindNextFile([In] FileHelper.SafeFindHandle hFindFile, out FileHelper.NativeMethods.WIN32_FIND_DATA lpFindFileData);

			// Token: 0x06005133 RID: 20787
			[DllImport("kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool FindClose([In] IntPtr hFindFile);

			// Token: 0x06005134 RID: 20788
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern FileHelper.NativeMethods.NTSTATUS LsaOpenPolicy(IntPtr systemName, ref FileHelper.NativeMethods.LSA_OBJECT_ATTRIBUTES attributes, int accessMask, out FileHelper.SafeLsaPolicyHandle handle);

			// Token: 0x06005135 RID: 20789
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern FileHelper.NativeMethods.NTSTATUS LsaClose(IntPtr ObjectHandle);

			// Token: 0x06005136 RID: 20790
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern FileHelper.NativeMethods.NTSTATUS LsaFreeMemory(IntPtr Buffer);

			// Token: 0x06005137 RID: 20791
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern FileHelper.NativeMethods.NTSTATUS LsaLookupNames([In] FileHelper.SafeLsaPolicyHandle PolicyHandle, [In] int Count, [In] FileHelper.NativeMethods.LSA_UNICODE_STRING[] Names, out FileHelper.SafeLsaMemoryHandle ReferencedDomains, out FileHelper.SafeLsaMemoryHandle Sids);

			// Token: 0x06005138 RID: 20792
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern FileHelper.NativeMethods.NTSTATUS LsaLookupSids([In] FileHelper.SafeLsaPolicyHandle PolicyHandle, [In] int Count, [In] IntPtr[] Sids, out FileHelper.SafeLsaMemoryHandle ReferencedDomains, out FileHelper.SafeLsaMemoryHandle Names);

			// Token: 0x06005139 RID: 20793
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int LsaNtStatusToWinError(FileHelper.NativeMethods.NTSTATUS status);

			// Token: 0x0600513A RID: 20794
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = true, ThrowOnUnmappableChar = true)]
			public static extern SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, IntPtr pSecurityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

			// Token: 0x0600513B RID: 20795
			[DllImport("ntdll.dll")]
			public static extern int NtQueryInformationFile(SafeFileHandle fileHandle, ref FileHelper.NativeMethods.IO_STATUS_BLOCK IoStatusBlock, IntPtr pInfoBlock, uint length, FileHelper.NativeMethods.FILE_INFORMATION_CLASS fileInformation);

			// Token: 0x0600513C RID: 20796
			[DllImport("ntdll.dll")]
			public static extern int RtlNtStatusToDosError(int status);

			// Token: 0x04002BD8 RID: 11224
			public static int FILE_READ_ATTRIBUTES = 128;

			// Token: 0x04002BD9 RID: 11225
			public static int FILE_FLAG_BACKUP_SEMANTICS = 33554432;

			// Token: 0x04002BDA RID: 11226
			public static uint IO_REPARSE_TAG_SYMLINK = 2684354572U;

			// Token: 0x04002BDB RID: 11227
			public static uint IO_REPARSE_TAG_MOUNT_POINT = 2684354563U;

			// Token: 0x02000B75 RID: 2933
			[BestFitMapping(false)]
			[Serializable]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct WIN32_FIND_DATA
			{
				// Token: 0x04002BDC RID: 11228
				public uint dwFileAttributes;

				// Token: 0x04002BDD RID: 11229
				public uint ftCreationTimeLow;

				// Token: 0x04002BDE RID: 11230
				public uint ftCreationTimeHigh;

				// Token: 0x04002BDF RID: 11231
				public uint ftLastAccessTimeLow;

				// Token: 0x04002BE0 RID: 11232
				public uint ftLastAccessTimeHigh;

				// Token: 0x04002BE1 RID: 11233
				public uint ftLastWriteTimeLow;

				// Token: 0x04002BE2 RID: 11234
				public uint ftLastWriteTimeHigh;

				// Token: 0x04002BE3 RID: 11235
				public uint nFileSizeHigh;

				// Token: 0x04002BE4 RID: 11236
				public uint nFileSizeLow;

				// Token: 0x04002BE5 RID: 11237
				public uint dwReserved0;

				// Token: 0x04002BE6 RID: 11238
				public uint dwReserved1;

				// Token: 0x04002BE7 RID: 11239
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
				public string cFileName;

				// Token: 0x04002BE8 RID: 11240
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
				public string cAlternateFileName;
			}

			// Token: 0x02000B76 RID: 2934
			public enum NTSTATUS
			{
				// Token: 0x04002BEA RID: 11242
				SUCCESS
			}

			// Token: 0x02000B77 RID: 2935
			public enum SID_NAME_USE
			{
				// Token: 0x04002BEC RID: 11244
				SidTypeUser = 1,
				// Token: 0x04002BED RID: 11245
				SidTypeGroup,
				// Token: 0x04002BEE RID: 11246
				SidTypeDomain,
				// Token: 0x04002BEF RID: 11247
				SidTypeAlias,
				// Token: 0x04002BF0 RID: 11248
				SidTypeWellKnownGroup,
				// Token: 0x04002BF1 RID: 11249
				SidTypeDeletedAccount,
				// Token: 0x04002BF2 RID: 11250
				SidTypeInvalid,
				// Token: 0x04002BF3 RID: 11251
				SidTypeUnknown,
				// Token: 0x04002BF4 RID: 11252
				SidTypeComputer
			}

			// Token: 0x02000B78 RID: 2936
			[Flags]
			public enum PolicyRights
			{
				// Token: 0x04002BF6 RID: 11254
				POLICY_VIEW_LOCAL_INFORMATION = 1,
				// Token: 0x04002BF7 RID: 11255
				POLICY_VIEW_AUDIT_INFORMATION = 2,
				// Token: 0x04002BF8 RID: 11256
				POLICY_GET_PRIVATE_INFORMATION = 4,
				// Token: 0x04002BF9 RID: 11257
				POLICY_TRUST_ADMIN = 8,
				// Token: 0x04002BFA RID: 11258
				POLICY_CREATE_ACCOUNT = 16,
				// Token: 0x04002BFB RID: 11259
				POLICY_CREATE_SECRET = 32,
				// Token: 0x04002BFC RID: 11260
				POLICY_CREATE_PRIVILEGE = 64,
				// Token: 0x04002BFD RID: 11261
				POLICY_SET_DEFAULT_QUOTA_LIMITS = 128,
				// Token: 0x04002BFE RID: 11262
				POLICY_SET_AUDIT_REQUIREMENTS = 256,
				// Token: 0x04002BFF RID: 11263
				POLICY_AUDIT_LOG_ADMIN = 512,
				// Token: 0x04002C00 RID: 11264
				POLICY_SERVER_ADMIN = 1024,
				// Token: 0x04002C01 RID: 11265
				POLICY_LOOKUP_NAMES = 2048,
				// Token: 0x04002C02 RID: 11266
				POLICY_NOTIFICATION = 4096
			}

			// Token: 0x02000B79 RID: 2937
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct LSA_UNICODE_STRING
			{
				// Token: 0x04002C03 RID: 11267
				public ushort Length;

				// Token: 0x04002C04 RID: 11268
				public ushort MaximumLength;

				// Token: 0x04002C05 RID: 11269
				[MarshalAs(UnmanagedType.LPWStr)]
				public string Buffer;
			}

			// Token: 0x02000B7A RID: 2938
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct LSA_TRANSLATED_NAME
			{
				// Token: 0x04002C06 RID: 11270
				public FileHelper.NativeMethods.SID_NAME_USE Use;

				// Token: 0x04002C07 RID: 11271
				public FileHelper.NativeMethods.LSA_UNICODE_STRING Name;

				// Token: 0x04002C08 RID: 11272
				public int DomainIndex;
			}

			// Token: 0x02000B7B RID: 2939
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct LSA_TRANSLATED_SID
			{
				// Token: 0x04002C09 RID: 11273
				public FileHelper.NativeMethods.SID_NAME_USE Use;

				// Token: 0x04002C0A RID: 11274
				public uint RelativeId;

				// Token: 0x04002C0B RID: 11275
				public int DomainIndex;
			}

			// Token: 0x02000B7C RID: 2940
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct LSA_OBJECT_ATTRIBUTES
			{
				// Token: 0x04002C0C RID: 11276
				public int Length;

				// Token: 0x04002C0D RID: 11277
				public IntPtr RootDirectory;

				// Token: 0x04002C0E RID: 11278
				public IntPtr ObjectName;

				// Token: 0x04002C0F RID: 11279
				public int Attributes;

				// Token: 0x04002C10 RID: 11280
				public IntPtr SecurityDescriptor;

				// Token: 0x04002C11 RID: 11281
				public IntPtr SecurityQualityOfService;
			}

			// Token: 0x02000B7D RID: 2941
			public struct IO_STATUS_BLOCK
			{
				// Token: 0x04002C12 RID: 11282
				public uint status;

				// Token: 0x04002C13 RID: 11283
				public ulong information;
			}

			// Token: 0x02000B7E RID: 2942
			public enum FILE_INFORMATION_CLASS
			{
				// Token: 0x04002C15 RID: 11285
				FileDirectoryInformation = 1,
				// Token: 0x04002C16 RID: 11286
				FileFullDirectoryInformation,
				// Token: 0x04002C17 RID: 11287
				FileBothDirectoryInformation,
				// Token: 0x04002C18 RID: 11288
				FileBasicInformation,
				// Token: 0x04002C19 RID: 11289
				FileStandardInformation,
				// Token: 0x04002C1A RID: 11290
				FileInternalInformation,
				// Token: 0x04002C1B RID: 11291
				FileEaInformation,
				// Token: 0x04002C1C RID: 11292
				FileAccessInformation,
				// Token: 0x04002C1D RID: 11293
				FileNameInformation,
				// Token: 0x04002C1E RID: 11294
				FileRenameInformation,
				// Token: 0x04002C1F RID: 11295
				FileLinkInformation,
				// Token: 0x04002C20 RID: 11296
				FileNamesInformation,
				// Token: 0x04002C21 RID: 11297
				FileDispositionInformation,
				// Token: 0x04002C22 RID: 11298
				FilePositionInformation,
				// Token: 0x04002C23 RID: 11299
				FileFullEaInformation,
				// Token: 0x04002C24 RID: 11300
				FileModeInformation,
				// Token: 0x04002C25 RID: 11301
				FileAlignmentInformation,
				// Token: 0x04002C26 RID: 11302
				FileAllInformation,
				// Token: 0x04002C27 RID: 11303
				FileAllocationInformation,
				// Token: 0x04002C28 RID: 11304
				FileEndOfFileInformation,
				// Token: 0x04002C29 RID: 11305
				FileAlternateNameInformation,
				// Token: 0x04002C2A RID: 11306
				FileStreamInformation,
				// Token: 0x04002C2B RID: 11307
				FilePipeInformation,
				// Token: 0x04002C2C RID: 11308
				FilePipeLocalInformation,
				// Token: 0x04002C2D RID: 11309
				FilePipeRemoteInformation,
				// Token: 0x04002C2E RID: 11310
				FileMailslotQueryInformation,
				// Token: 0x04002C2F RID: 11311
				FileMailslotSetInformation,
				// Token: 0x04002C30 RID: 11312
				FileCompressionInformation,
				// Token: 0x04002C31 RID: 11313
				FileObjectIdInformation,
				// Token: 0x04002C32 RID: 11314
				FileCompletionInformation,
				// Token: 0x04002C33 RID: 11315
				FileMoveClusterInformation,
				// Token: 0x04002C34 RID: 11316
				FileQuotaInformation,
				// Token: 0x04002C35 RID: 11317
				FileReparsePointInformation,
				// Token: 0x04002C36 RID: 11318
				FileNetworkOpenInformation,
				// Token: 0x04002C37 RID: 11319
				FileAttributeTagInformation,
				// Token: 0x04002C38 RID: 11320
				FileTrackingInformation,
				// Token: 0x04002C39 RID: 11321
				FileIdBothDirectoryInformation,
				// Token: 0x04002C3A RID: 11322
				FileIdFullDirectoryInformation,
				// Token: 0x04002C3B RID: 11323
				FileValidDataLengthInformation,
				// Token: 0x04002C3C RID: 11324
				FileShortNameInformation,
				// Token: 0x04002C3D RID: 11325
				FileHardLinkInformation = 46
			}

			// Token: 0x02000B7F RID: 2943
			[StructLayout(LayoutKind.Explicit)]
			public struct FILE_BASIC_INFORMATION
			{
				// Token: 0x04002C3E RID: 11326
				[FieldOffset(0)]
				public long CreationTime;

				// Token: 0x04002C3F RID: 11327
				[FieldOffset(8)]
				public long LastAccessTime;

				// Token: 0x04002C40 RID: 11328
				[FieldOffset(16)]
				public long LastWriteTime;

				// Token: 0x04002C41 RID: 11329
				[FieldOffset(24)]
				public long ChangeTime;

				// Token: 0x04002C42 RID: 11330
				[FieldOffset(32)]
				public ulong FileAttributes;
			}
		}

		// Token: 0x02000B80 RID: 2944
		public sealed class AccessControlEntriesCache
		{
			// Token: 0x0600513E RID: 20798 RVA: 0x001105B5 File Offset: 0x0010E7B5
			public AccessControlEntriesCache()
			{
				this.identityCache = new Dictionary<IdentityReference, FunctionValue>();
			}

			// Token: 0x0600513F RID: 20799 RVA: 0x001105C8 File Offset: 0x0010E7C8
			public bool TryCreateIdentityCondition(IdentityReference identity, out FunctionValue condition)
			{
				if (!this.identityCache.TryGetValue(identity, out condition))
				{
					FileHelper.IdentityKind identityKind;
					if (FileHelper.TryGetIdentityKind(identity, out identityKind))
					{
						if (identityKind != FileHelper.IdentityKind.User)
						{
							if (identityKind == FileHelper.IdentityKind.Group)
							{
								condition = new AccessControlEntriesModule.AccessControlEntry.IsMemberOfConditionFunctionValue(TextValue.New(identity.Value));
							}
						}
						else
						{
							condition = new AccessControlEntriesModule.AccessControlEntry.UserEqualsConditionFunctionValue(TextValue.New(identity.Value));
						}
					}
					this.identityCache.Add(identity, condition);
				}
				return condition != null;
			}

			// Token: 0x04002C43 RID: 11331
			private readonly Dictionary<IdentityReference, FunctionValue> identityCache;
		}

		// Token: 0x02000B81 RID: 2945
		private class SafeLsaPolicyHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x06005140 RID: 20800 RVA: 0x0000EDD3 File Offset: 0x0000CFD3
			public SafeLsaPolicyHandle()
				: base(true)
			{
			}

			// Token: 0x06005141 RID: 20801 RVA: 0x00110630 File Offset: 0x0010E830
			protected override bool ReleaseHandle()
			{
				return FileHelper.NativeMethods.LsaClose(this.handle) == FileHelper.NativeMethods.NTSTATUS.SUCCESS;
			}
		}

		// Token: 0x02000B82 RID: 2946
		private class SafeLsaMemoryHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x17001946 RID: 6470
			// (get) Token: 0x06005142 RID: 20802 RVA: 0x00110640 File Offset: 0x0010E840
			public static FileHelper.SafeLsaMemoryHandle InvalidHandle
			{
				get
				{
					return new FileHelper.SafeLsaMemoryHandle(IntPtr.Zero);
				}
			}

			// Token: 0x06005143 RID: 20803 RVA: 0x0000EDD3 File Offset: 0x0000CFD3
			public SafeLsaMemoryHandle()
				: base(true)
			{
			}

			// Token: 0x06005144 RID: 20804 RVA: 0x0011064C File Offset: 0x0010E84C
			private SafeLsaMemoryHandle(IntPtr handle)
				: base(true)
			{
				base.SetHandle(handle);
			}

			// Token: 0x06005145 RID: 20805 RVA: 0x0011065C File Offset: 0x0010E85C
			protected override bool ReleaseHandle()
			{
				return FileHelper.NativeMethods.LsaFreeMemory(this.handle) == FileHelper.NativeMethods.NTSTATUS.SUCCESS;
			}
		}
	}
}
