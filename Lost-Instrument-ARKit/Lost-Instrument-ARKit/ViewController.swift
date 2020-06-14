//
//  ViewController.swift
//  Lost-Instrument-ARKit
//
//  Created by 齐旭晨 on 2020/6/13.
//  Copyright © 2020 齐旭晨. All rights reserved.
//

import UIKit
import SceneKit
import ARKit

class ViewController: UIViewController {

    @IBOutlet var sceneView: ARSCNView!
    
    var focusSquare: FocusSquare?
    var screenCenter: CGPoint!
    
    var modelsInTheScene: Array<SCNNode> = []
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        // Set the view's delegate
        sceneView.delegate = self
        
        // Show statistics such as fps and timing information
        sceneView.showsStatistics = false
        sceneView.autoenablesDefaultLighting = true
        sceneView.automaticallyUpdatesLighting = true
        
        screenCenter = view.center
        
        // Create a new scene
//        let scene = SCNScene(named: "art.scnassets/eh.scn")!
        
        // Set the scene to the view
//        sceneView.scene = scene
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        
        // Create a session configuration
        let configuration = ARWorldTrackingConfiguration()
        configuration.planeDetection = .horizontal

        // Run the view's session
        sceneView.session.run(configuration)
    }
    
    override func viewWillDisappear(_ animated: Bool) {
        super.viewWillDisappear(animated)
        
        // Pause the view's session
        sceneView.session.pause()
    }
    
    override func viewWillTransition(to size: CGSize, with coordinator: UIViewControllerTransitionCoordinator) {
        super.viewWillTransition(to: size, with: coordinator)
        let viewCenter = CGPoint(x: size.width / 2, y: size.height / 2)
        screenCenter = viewCenter
    }
    
    func updateFocusSquare(){
        guard let focusSquareLocal = focusSquare else{return}
        
        let hitTest = sceneView.hitTest(screenCenter, types: .existingPlaneUsingExtent)
        if let hitTestResult = hitTest.first{
            print("Focus square hits a plane")
            
            let canAddNewModel = hitTestResult.anchor is ARPlaneAnchor
            focusSquareLocal.isClosed = canAddNewModel
        }else{
            print("Focus square does not hit a plane")
            
            focusSquareLocal.isClosed = false
        }
    }
    
    func session(_ session: ARSession, didFailWithError error: Error) {
        // Present an error message to the user
        
    }
    
    func sessionWasInterrupted(_ session: ARSession) {
        // Inform the user that the session has been interrupted, for example, by presenting an overlay
        
    }
    
    func sessionInterruptionEnded(_ session: ARSession) {
        // Reset tracking and/or remove existing anchors if consistent tracking is required
        
    }
}
