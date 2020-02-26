default_platform(:ios)
platform :ios do
  lane :make_app do

     update_project_provisioning(
         xcodeproj: "Unity-iPhone.xcodeproj",
         profile: "/Users/ttt/Test.mobileprovision", # optional if you use sigh
    #     target_filter: ".*WatchKit Extension.*", # matches name or type of a target
          build_configuration: "Release",
    #      code_signing_identity: "iPhone Development" # optionally specify the codesigning identity
      )

      xcbuild(
          project: "Unity-iPhone.xcodeproj",
          scheme: "Unity-iPhone",
          configuration: "Release",
          xcargs: "SYMROOT='/var/tmp/' CONFIGURATION_BUILD_DIR='$(PROJECT_DIR)/out/'"
      )
     end
end