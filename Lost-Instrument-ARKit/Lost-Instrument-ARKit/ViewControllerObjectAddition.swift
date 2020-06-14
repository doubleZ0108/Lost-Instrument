//
//  ViewControllerObjectAddition.swift
//  Lost-Instrument-ARKit
//
//  Created by 齐旭晨 on 2020/6/13.
//  Copyright © 2020 齐旭晨. All rights reserved.
//

import UIKit
import SceneKit
import ARKit

extension ViewController {
    
    fileprivate func getModel(named name: String) -> SCNNode?{
        let scene = SCNScene(named: "art.scnassets/ship.scn")
        
        guard let model = scene?.rootNode.childNode(withName: "ship", recursively: false) else{return nil}
                
        model.name = name
        
        return model
    }
    
    @IBAction func addObjectButtonTapped(_ sender: Any) {
        print("Add Button clicked")
        
        guard focusSquare != nil else{return}
        
        let modelName = "ship"
        guard let model = getModel(named: modelName) else{
            print("Unable to load \(modelName)")
            return
        }
        
        let hitTest = sceneView.hitTest(screenCenter, types: .existingPlaneUsingExtent)
        guard let worldTransformColum3 = hitTest.first?.worldTransform.columns.3 else{ return }
        model.position = SCNVector3(worldTransformColum3.x, worldTransformColum3.y, worldTransformColum3.z)
        
        sceneView.scene.rootNode.addChildNode(model)
        print("\(modelName) added successfully")
        
        modelsInTheScene.append(model)
        print("Currently have \(modelsInTheScene.count) model(s) in the scene" )
    }
    
}
