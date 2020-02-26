mkdir ./ios_output/proj/fastlane/
OUT_FILE="./ios_output/proj/fastlane/Fastfile"


printf "default_platform(:ios)\n" > $OUT_FILE

printf "platform :ios do\n" >> $OUT_FILE

printf "  lane :make_app do\n" >> $OUT_FILE
  
#printf "#  set_info_plist_value(path: "./Info.plist", key: "CFBundleVersion", value: ENV["BUILD_NUMBER"])\n" >> $OUT_FILE


printf "      xcbuild(\n" >> $OUT_FILE
printf "          workspace: \"Unity-iPhone.xcworkspace\",\n" >> $OUT_FILE
printf "          scheme: \"Unity-iPhone\",\n" >> $OUT_FILE
printf "          configuration: \"Debug\",\n" >> $OUT_FILE
printf "          xcargs: \"-sdk iphonesimulator SYMROOT='/var/tmp/' LIBRARY_SEARCH_PATHS='\$(inherited) \$(PROJECT_DIR)/Libraries/' CONFIGURATION_BUILD_DIR='\$(PROJECT_DIR)/out/'\"\n" >> $OUT_FILE
printf "      )\n" >> $OUT_FILE
printf "     end\n" >> $OUT_FILE

printf "end\n" >> $OUT_FILE
