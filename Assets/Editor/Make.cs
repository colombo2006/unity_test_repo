using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace Commander.Editor { 
    public static class PackageBuilder {
        private const string APK_NAME = "game.apk";
        private const string XCODE_PROJ = "proj";

        private static string mode;
        private static string scene;
        private static string keystorePath;
        private static string keystorePassword;
        private static string aliasName;
        private static string aliasPassword;
        private static string appIdentifier;
        private static string appName;
        private static string companyName;
        private static string version;
        private static int versionCode;

        static PackageBuilder() {
            mode = "DEVELOPMENT";
            keystorePath = PlayerSettings.Android.keystoreName;
            keystorePassword = PlayerSettings.Android.keyaliasPass;
            aliasName = PlayerSettings.Android.keyaliasName;
            aliasPassword = PlayerSettings.Android.keyaliasPass;
            version = PlayerSettings.bundleVersion;
            versionCode = PlayerSettings.Android.bundleVersionCode;
            appIdentifier = PlayerSettings.applicationIdentifier;
            appName = PlayerSettings.productName;
            companyName = PlayerSettings.companyName;
        }

        public static void BatchBuildAndroid() {

            var ini_file = new IniFile2("config.ini");
            mode = ini_file.ReadINI("Config", "MODE");
            keystorePath = ini_file.ReadINI("Config", "KEYSTORE_PATH");
            keystorePassword = ini_file.ReadINI("Config", "KEYSTORE_PASSWORD");
            aliasPassword = ini_file.ReadINI("Config", "ALIAS_PASSWORD");
            appIdentifier = ini_file.ReadINI("Config", "APP_IDENTIFIER");
            appName = ini_file.ReadINI("Config", "APP_NAME");
            companyName = ini_file.ReadINI("Config", "COMPANY_NAME");
            version = ini_file.ReadINI("Config", "APP_VERSION");
            versionCode = Convert.ToInt32(ini_file.ReadINI("Config", "APP_VERSION_CODE"));
            aliasName = ini_file.ReadINI("Config", "ALIAS");

            Debug.LogWarning("MODE\t\t\t" + mode);
            Debug.LogWarning("KEYSTORE_PATH\t\t\t" + keystorePath);
            Debug.LogWarning("KEYSTORE_PASSWORD\t\t\t" + keystorePassword);
            Debug.LogWarning("ALIAS_PASSWORD\t\t\t" + aliasPassword);
            Debug.LogWarning("APP_IDENTIFIER\t\t\t" + appIdentifier);
            Debug.LogWarning("APP_NAME\t\t\t" + appName);
            Debug.LogWarning("COMPANY_NAME\t\t\t" + companyName);
            Debug.LogWarning("APP_VERSION\t\t\t" + version);
            Debug.LogWarning("APP_VERSION_CODE\t\t\t" + ini_file.ReadINI("Config", "APP_VERSION_CODE"));
            Debug.LogWarning("ALIAS\t\t\t" + aliasName);

            if (string.IsNullOrEmpty(keystorePath) || string.IsNullOrEmpty(keystorePassword) || string.IsNullOrEmpty(aliasName) || string.IsNullOrEmpty(aliasPassword)) {
                Debug.LogError("Keystore is required for Android build");
                EditorApplication.Exit(1);
                return;
            }
            int code = BuildAndroid();
            EditorApplication.Exit(code);
        }

        public static void BatchBuildIos()
        {

            var ini_file = new IniFile2("config.ini");
            mode = ini_file.ReadINI("Config", "MODE");
            scene = ini_file.ReadINI("Scene", "Assets\\Scenes\\SampleScene.unity");

//            appIdentifier = ini_file.ReadINI("Config", "APP_IDENTIFIER");
//            appName = ini_file.ReadINI("Config", "APP_NAME");
//            companyName = ini_file.ReadINI("Config", "COMPANY_NAME");
//            version = ini_file.ReadINI("Config", "APP_VERSION");
//            versionCode = Convert.ToInt32(ini_file.ReadINI("Config", "APP_VERSION_CODE"));

            Debug.LogWarning("MODE\t\t\t" + mode);
            Debug.LogWarning("Scene\t\t\t" + scene);

            int code = BuildIos();
            EditorApplication.Exit(code);
        }

        [MenuItem("Commander/Build/Android")]
        public static int BuildAndroid() {
            string[] scenes = { scene };
            BuildOptions buildOptions = BuildOptions.None;
            if (mode != "PRODUCTION")
                buildOptions = buildOptions | BuildOptions.Development | BuildOptions.AllowDebugging;

            PlayerSettings.Android.keystoreName = keystorePath;
            PlayerSettings.Android.keystorePass = keystorePassword;
            PlayerSettings.Android.keyaliasName = aliasName;
            PlayerSettings.Android.keyaliasPass = aliasPassword;
            PlayerSettings.Android.bundleVersionCode = versionCode;
            PlayerSettings.bundleVersion = version;
            PlayerSettings.applicationIdentifier = appIdentifier;
            PlayerSettings.productName = appName;
            PlayerSettings.companyName = companyName;
            BuildReport report = BuildPipeline.BuildPlayer(scenes, $"android_output{Path.DirectorySeparatorChar}{APK_NAME}", BuildTarget.Android, buildOptions);
            if (report.summary.result != BuildResult.Succeeded) {
                Debug.LogError("Build Failed");
                return 1;
            }
            return 0;
        }

        [MenuItem("Commander/Build/iOS")]
        public static int BuildIos()
        {
            string[] scenes = { "Assets/_Scenes/Main.unity" };
            BuildOptions buildOptions = BuildOptions.None;
            if (mode != "PRODUCTION")
                buildOptions = buildOptions | BuildOptions.Development | BuildOptions.AllowDebugging;

            PlayerSettings.bundleVersion = version;
            PlayerSettings.applicationIdentifier = appIdentifier;
            PlayerSettings.productName = appName;
            PlayerSettings.companyName = companyName;
            BuildReport report = BuildPipeline.BuildPlayer(scenes, $"ios_output{Path.DirectorySeparatorChar}{XCODE_PROJ}", BuildTarget.iOS, buildOptions);
            if (report.summary.result != BuildResult.Succeeded)
            {
                Debug.LogError("Build Failed");
                return 1;
            }
            return 0;
        }
    }
}
