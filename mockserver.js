#!/usr/bin/env node

/*
 * Mock server used instead of WebCORE to test the Unity code.
 * 
 * Setup:
 * Install/update `node` and `npm` if needed, then run `npm i` to install dependencies
 * 
 * Run:
 * `node mockserver.js` or `./mockserver.js` on *nix
 */

const express = require('express');
const app = express();

app.use(express.json());
app.use(express.urlencoded({ extended: true }));
const cors = require("cors");
const corsOptions = {
  origin: ['http://localhost:8080', 'http://localhost', 'http://127.0.0.1'],
  methods: ['GET', 'POST', 'DELETE', 'UPDATE', 'PUT', 'PATCH'],
  credentials: true,            //access-control-allow-credentials:true
  optionSuccessStatus: 200,
}

app.use(cors(corsOptions)); // Use this after the variable declaration

const SUCCESS = 200;
const PORT = 8080;

const CDM_NAME = "MULTIPLE_CLASSES";

var classDiagram = {
  "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//ClassDiagram",
  "_id": "100",
  "name": CDM_NAME,
  "classes": [
    {
      "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//Class",
      "_id": "1",
      "name": "Class1",
      "attributes": [{
        "_id": "1",   // id = "1" as first ID, part of counterAttributeID implementation
        "name": "year",
        "type": "6"
      }, {
        "_id": "2",   // id = "2" as second ID, part of counterAttributeID implementation
        "name": "month",
        "type": "8"
      }]
    },
    {
      "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//Class",
      "_id": "2",
      "name": "Class2",
      "attributes": []
    }
  ],
  "types": [
    {
      "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//CDVoid",
      "_id": "2"
    },
    {
      "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//CDAny",
      "_id": "3"
    },
    {
      "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//CDBoolean",
      "_id": "4"
    },
    {
      "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//CDDouble",
      "_id": "5"
    },
    {
      "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//CDInt",
      "_id": "6"
    },
    {
      "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//CDLong",
      "_id": "7"
    },
    {
      "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//CDString",
      "_id": "8"
    },
    {
      "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//CDByte",
      "_id": "9"
    },
    {
      "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//CDFloat",
      "_id": "10"
    },
    {
      "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//CDChar",
      "_id": "11"
    }
  ],
  "layout": {
    "_id": "12",
    "containers": [
      {
        "_id": "13",
        "key": "null",
        "value": [ // TODO Change to "values" when WebCORE is updated
          {
            "_id": "14",
            "key": "1", // is the same as the class id
            "value": {
              "_id": "15",
              "x": 365.5,
              "y": 300.0
            }
          },
          {
            "_id": "15",
            "key": "2",
            "value": {
              "_id": "105",
              "x": 565.5,
              "y": 180.0
            }
          }
        ]
      }
    ]
  }
};

//TODO: unsure what these ids are for, hard coded for now
var valueId = 16;
var valueValueId = 106;
var counterAttributeId = 3; // counter for ading attributes, for having unique IDs for each attribute

// GET class diagram
app.get('/classdiagram/:cdmName', (req, res) => {
  //console.log(classDiagram);
  console.log(JSON.stringify(classDiagram, null, 4));
  res.json(classDiagram); // TODO change
  // res.sendStatus(SUCCESS);
});

// Add class
app.post('/classdiagram/:cdmName/class', (req, res) => {
  const className = req.body.className;
  const xPos = req.body.x;
  const yPos = req.body.y;

  const allClassIds = classDiagram.classes.map(c => c._id);
  const newClassId = (parseInt(allClassIds[allClassIds.length - 1]) + 1).toString();

  classDiagram.classes.push({
    "eClass": "http://cs.mcgill.ca/sel/cdm/1.0#//Class",
    "_id": newClassId,
    "name": className,
    "attributes": []
  })

  classDiagram.layout.containers[0].value.push({
    "_id": valueId,
    "key": newClassId,
    "value": {
      "_id": valueValueId,
      "x": xPos,
      "y": yPos
    }
  })

  valueId += 1;
  valueValueId += 1;

  console.log(classDiagram);
  console.log(">>> Added class given req.body: " + JSON.stringify(req.body));
  res.sendStatus(SUCCESS);
});

//Update class position
app.put('/classdiagram/:cdmName/:classId/position', (req, res) => {
  const classId = req.params.classId;
  var values = classDiagram.layout.containers[0].value/*s*/; // TODO Change to "values" later
  // retrieve name of class with updated position
  const allClassIds = classDiagram.classes.map(c => c._id);
  var classIndex = allClassIds.indexOf(classId);
  var className = classDiagram.classes[classIndex].name;
  //update position details
  const allLayoutIds = values.map(c => c.key);
  var index = allLayoutIds.indexOf(classId);
  var value = classDiagram.layout.containers[0].value[index].value;
  value.x = req.body.xPosition;
  value.y = req.body.yPosition;
  console.log(`>>> Updated ${className} position = x: ${value.x}, y: ${value.y}`);
  res.sendStatus(SUCCESS);
});

// Delete class
app.delete('/classdiagram/:cdmName/class/:class_id', (req, res) => {
  const classId = req.params.class_id;
  const allClassIds = classDiagram.classes.map(c => c._id);
  var indexToRemove = allClassIds.indexOf(classId);
  if (indexToRemove > -1) {
    classDiagram.classes.splice(indexToRemove, 1);
  }

  var values = classDiagram.layout.containers[0].value/*s*/; // TODO Change to "values" later
  const allLayoutIds = values.map(c => c.key);
  var indexToRemove2 = allLayoutIds.indexOf(classId);
  if (indexToRemove2 > -1) {
    values.splice(indexToRemove2, 1);
  }
  console.log(classDiagram);
  console.log(`>>>>> Deleted class with id: ${classId}`);
  res.sendStatus(SUCCESS);
});

// Delete Attribute
app.delete('/classdiagram/:cdmName/class/attributes/:attributeId', (req,res) => {
  const AttributeId = req.params.attributeId;
  
  for (var i = 0; i < classDiagram.classes.length; i++) {
    for (var j = 0; j < classDiagram.classes[i].attributes.length; j++) {
      if (classDiagram.classes[i].attributes[j]._id == AttributeId) {
        classDiagram.classes[i].attributes.splice(j, 1)
      }
    }
  }

  //console.log(">>> Attributes at ClassOne: " + classDiagram.classes.attributes[0]);
  // util.inspect()
  res.sendStatus(SUCCESS);
});

// Add attribute
app.post('/classdiagram/:cdmName/class/:classId/attribute', (req, res) => {
  //console.log("HEHFJHFJEHFKJEHFKEJKFJEKFJEKFJEKFJEKFJJKFEJF");
  const classId = req.params.classId;
  const attributeName = req.body.attributeName;
  const rankIndex = req.body.rankIndex;
  const typeId = req.body.typeId;
  // @param body {"rankIndex": Integer, "typeId": Integer, "attributeName": String}
  
  for (var i = 0; i < classDiagram.classes.length; i++) {
    if (classDiagram.classes[i]._id == classId) {
      classDiagram.classes[i].attributes.push({
        "_id": counterAttributeId.toString(),
         "name": attributeName,
         "type": typeId,
      })
      counterAttributeId += 1;
    }
  }

  console.log(">>> Added attribute given req.body: " + JSON.stringify(req.body));

  res.sendStatus(SUCCESS);
});

// Give feedback (work in progress)
app.get("/classdiagram/:cdmName/feedback", (req, res) => {

});

/** Gets the item with the given _id by recursing into the iterable. */
function getById(id, iterable) {
  if (Array.isArray(iterable)) {
    for (var item of iterable) {
      if (id == item["_id"]) {
        return item;
      } else {
        result = getById(id, item);
        if (result) {
          return result;
        }
      }
    }
  } else if (typeof iterable === "object") {
    for (var key in iterable) {
      if (key == "_id" && id == iterable[key]) {
        return iterable;
      } else {
        result = getById(id, iterable[key]);
        if (result) {
          return result;
        }
      }
    }
  }
  return null;
}

const server = app.listen(PORT, () => {
  var host = server.address().address;
  var port = server.address().port;
  console.log(`Mock WebCORE app listening at http://${host}:${port}`);
});
