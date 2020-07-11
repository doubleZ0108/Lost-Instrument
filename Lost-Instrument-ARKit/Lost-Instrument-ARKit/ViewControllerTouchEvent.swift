//
//  ViewControllerTouchEvent.swift
//  Lost-Instrument-ARKit
//
//  Created by 齐旭晨 on 2020/7/9.
//  Copyright © 2020 齐旭晨. All rights reserved.
//

import Foundation
import EventKit
import ARKit

var isInstrumentsExpanded = Dictionary<SCNNode, Bool>()
var isAnimating = false

extension ViewController{
    override func touchesBegan(_ touches: Set<UITouch>, with event: UIEvent?) {
        
        guard !isAnimating else{
            return
        }
        
        let touch = touches.first!
        
        if touch.view == self.sceneView{
            let viewTouchLocation:CGPoint = touch.location(in: sceneView)
            
            guard let node = sceneView.hitTest(viewTouchLocation, options: nil).first?.node else{
                print("detection missed")
                focusSquare?.unhide()
                return
            }
            
            // get its material
            let material = node.geometry!.firstMaterial!
            
            // highlight it
            SCNTransaction.begin()
            SCNTransaction.animationDuration = 0.5
            
            // on completion - unhighlight
            SCNTransaction.completionBlock = {
                SCNTransaction.begin()
                SCNTransaction.animationDuration = 0.5
                
                material.emission.contents = UIColor.black
                
                SCNTransaction.commit()
            }
            
            material.emission.contents = UIColor.white
            SCNTransaction.commit()
            
            guard let parentNode = sceneView.scene.rootNode.childNode(withName: String(node.name!.split(separator: "-")[0]), recursively: false) else{
                return
            }
            
            // Assemble or disassemble
            if !isInstrumentsExpanded.keys.contains(parentNode){
                isInstrumentsExpanded[parentNode] = false
            }

            isAnimating = true
            DispatchQueue.main.asyncAfter(deadline: .now()+1, execute: {
                isAnimating = false
            })
            
            if isInstrumentsExpanded[parentNode]!{
                print("instrument: \(parentNode.name) expanded already")
                node.runActionAssemble(node: parentNode)
                isInstrumentsExpanded[parentNode] = false
            }else{
                node.runActionDisassemble(node: parentNode)
                isInstrumentsExpanded[parentNode] = true
            }
        }
    }
}

extension SCNNode{
    func runActionDisassemble(node: SCNNode) {
        print(node.name!)
        switch node.name!{
        case "erhu":
            for node in node.childNodes{
                switch node.name?.split(separator: "-")[1] {
                case "1":
                    node.runAction(SCNAction.moveBy(x: 0.1, y: 0, z: 0, duration: 1))
                    
                case "2":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0.06, duration: 1))
                case "3":
                    node.runAction(SCNAction.moveBy(x: 0.05, y: 0, z: 0, duration: 1))
                case "4":
                    node.runAction(SCNAction.moveBy(x: -0.1, y: 0, z: 0, duration: 1))
                case "5":
                    node.runAction(SCNAction.moveBy(x: -0.1, y: 0, z: 0, duration: 1))
                case "6":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0.01, duration: 1))
                case "7":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: -0.2, duration: 1))
                default:
                    print("Unknown Node of erhu")
                }
            }
        case "pipa":
            for node in node.childNodes{
                switch node.name?.split(separator: "-")[1] {
                case "1":
                    print(node.name)
                    print(node.name?.split(separator: "-")[1])
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0, duration: 1))
                case "2":
                    node.runAction(SCNAction.moveBy(x: -60, y: 0, z: 0, duration: 1))
                case "3":
                    node.runAction(SCNAction.moveBy(x: 0, y: -18, z: 0, duration: 1))
                case "4":
                    node.runAction(SCNAction.moveBy(x: 0, y: -60, z: 0, duration: 1))
                case "5":
                    node.runAction(SCNAction.moveBy(x: 60, y: 0, z: 0, duration: 1))
                case "6":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 60, duration: 1))
                case "7":
                    node.runAction(SCNAction.moveBy(x: 0, y: -12, z: 0, duration: 1))
                case "8":
                    node.runAction(SCNAction.moveBy(x: 0, y: 60, z: 12, duration: 1))
                case "9":
                    node.runAction(SCNAction.moveBy(x: 0, y: -60, z: 0, duration: 1))
                case "10":
                    node.runAction(SCNAction.moveBy(x: 0, y: 60, z: 0, duration: 1))
                case "11":
                    node.runAction(SCNAction.moveBy(x: 0, y: -60, z: 0, duration: 1))
                case "12":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0, duration: 1))
                default:
                    print("Unknown Node of pipa")
                }
            }
        case "yangqin":
            for node in node.childNodes{
                switch node.name?.split(separator: "-")[1] {
                case "1":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 60, duration: 1))
                case "2":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: -40, duration: 1))
                case "3":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0, duration: 1))
                case "4":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: -20, duration: 1))
                case "5":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: -60, duration: 1))
                case "6":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 20, duration: 1))
                case "7":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 40, duration: 1))
                case "8":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 10, duration: 1))
                case "9":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 10, duration: 1))
                default:
                    print("Unknown Node of yangqin")
                }
            }
        case "guzheng":
            for node in node.childNodes{
                switch node.name?.split(separator: "-")[1] {
                case "1":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0.2, duration: 1))
                case "2":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0.4, duration: 1))
                case "3":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: -0.4, duration: 1))
                case "4":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0.2, duration: 1))
                case "5":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: -0.2, duration: 1))
                case "6":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0, duration: 1))
                default:
                    print("Unknown Node of guzheng")
                }
            }
        default:
            print("Name unmatched")
        }
    }
    
    func runActionAssemble(node: SCNNode){
        print(node.name!)
        switch self.name!.split(separator: "-")[0]{
        case "erhu":
            for node in node.childNodes{
                switch node.name?.split(separator: "-")[1] {
                case "1":
                    //                        node.runAction(SCNAction.moveBy(x: 0.1, y: 0, z: 0, duration: 1))
                    node.runAction(SCNAction.moveBy(x: 0.1, y: 0, z: 0, duration: 1).reversed())
                    //                    isAnimating = true
                    //                    DispatchQueue.main.asyncAfter(deadline: .now()+1, execute: {
                    //                        isAnimating = false
                //                    })
                case "2":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0.06, duration: 1).reversed())
                case "3":
                    node.runAction(SCNAction.moveBy(x: 0.05, y: 0, z: 0, duration: 1).reversed())
                case "4":
                    node.runAction(SCNAction.moveBy(x: -0.1, y: 0, z: 0, duration: 1).reversed())
                case "5":
                    node.runAction(SCNAction.moveBy(x: -0.1, y: 0, z: 0, duration: 1).reversed())
                case "6":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0.01, duration: 1).reversed())
                case "7":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: -0.2, duration: 1).reversed())
                default:
                    print("Unknown Node")
                }
            }
        case "pipa":
            for node in node.childNodes{
                switch node.name?.split(separator: "-")[1] {
                case "1":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0, duration: 1).reversed())
                case "2":
                    node.runAction(SCNAction.moveBy(x: -60, y: 0, z: 0, duration: 1).reversed())
                case "3":
                    node.runAction(SCNAction.moveBy(x: 0, y: -18, z: 0, duration: 1).reversed())
                case "4":
                    node.runAction(SCNAction.moveBy(x: 0, y: -60, z: 0, duration: 1).reversed())
                case "5":
                    node.runAction(SCNAction.moveBy(x: 60, y: 0, z: 0, duration: 1).reversed())
                case "6":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 60, duration: 1).reversed())
                case "7":
                    node.runAction(SCNAction.moveBy(x: 0, y: -12, z: 0, duration: 1).reversed())
                case "8":
                    node.runAction(SCNAction.moveBy(x: 0, y: 60, z: 12, duration: 1).reversed())
                case "9":
                    node.runAction(SCNAction.moveBy(x: 0, y: -60, z: 0, duration: 1).reversed())
                case "10":
                    node.runAction(SCNAction.moveBy(x: 0, y: 60, z: 0, duration: 1).reversed())
                case "11":
                    node.runAction(SCNAction.moveBy(x: 0, y: -60, z: 0, duration: 1).reversed())
                case "12":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0, duration: 1).reversed())
                default:
                    print("Unknown Node of pipa")
                }
            }
        case "yangqin":
            for node in node.childNodes{
                switch node.name?.split(separator: "-")[1] {
                case "1":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 60, duration: 1).reversed())
                case "2":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: -40, duration: 1).reversed())
                case "3":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0, duration: 1).reversed())
                case "4":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: -20, duration: 1).reversed())
                case "5":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: -60, duration: 1).reversed())
                case "6":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 20, duration: 1).reversed())
                case "7":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 40, duration: 1).reversed())
                case "8":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 10, duration: 1).reversed())
                case "9":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 10, duration: 1).reversed())
                default:
                    print("Unknown Node of yangqin")
                }
            }
        case "guzheng":
            for node in node.childNodes{
                switch node.name?.split(separator: "-")[1] {
                case "1":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0.2, duration: 1).reversed())
                case "2":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0.4, duration: 1).reversed())
                case "3":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: -0.4, duration: 1).reversed())
                case "4":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0.2, duration: 1).reversed())
                case "5":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: -0.2, duration: 1).reversed())
                case "6":
                    node.runAction(SCNAction.moveBy(x: 0, y: 0, z: 0, duration: 1).reversed())
                default:
                    print("Unknown Node of guzheng")
                }
            }
        default:
            print("Name unmatched")
        }
    }
}
