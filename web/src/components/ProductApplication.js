import React, { useState } from "react";
import { Stepper, Step } from "react-form-stepper";
import { MdDescription } from "react-icons/md";
import { Link } from "react-router-dom";
import StepWizard from "react-step-wizard";
import { Row, Col, Button, FormGroup, Label, Input } from "reactstrap";

const ActionButtons = (props) => {
  const handleBack = () => {
    props.previousStep();
  };

  const handleNext = () => {
    props.nextStep();
  };

  const handleFinish = () => {
    props.lastStep();
  };

  return (
    <div>
      <Row>
        {props.currentStep > 1 && (
          <Col>
            <Button onClick={handleBack}>Back</Button>
          </Col>
        )}
        <Col>
          {props.currentStep < props.totalSteps && (
            <Button onClick={handleNext}>Next</Button>
          )}
          {props.currentStep === props.totalSteps && (
           <Link to="/products">
            <Button onClick={handleFinish}>Finish</Button>
           </Link>
          )}
        </Col>
      </Row>
    </div>
  );
};

const One = (props) => {
  const [info1, setInfo1] = useState({});
  const [error, setError] = useState("");

  const onInputChanged = (event) => {
    const targetName = event.target.name;
    const targetValue = event.target.value;

    // We append newly entered value to the existing state
    setInfo1((info1) => ({
      ...info1,
      [targetName]: targetValue
    }));
  };

  const validate = () => {
    if (!info1.selectGrant) setError("This is a mandatory field");
    else {
      setError("");
      props.nextStep();
      props.userCallback(info1);
    }
  };

  return (
    <div>
      <span style={{ color: "red" }}>{error}</span>
       <FormGroup>
        <Label for="selectGrantDropdown">
          Which type of student grant do you wish to apply for?
        </Label>
        <Input
          id="selectGrantDropdown"
          name="selectGrant"
          type="select"
          onChange={onInputChanged}
        >
          <option hidden>
            Select grant type
          </option>
          <option>
            Basic grant
          </option>
          <option>
            Supplementary grant
          </option>
        </Input>
      </FormGroup>
      <br />
      <ActionButtons {...props} nextStep={validate} />
    </div>
  );
};

const Two = (props) => {
  const [info2, setInfo2] = useState({});
  const [error, setError] = useState("");

  const onInputChanged = (event) => {
    const targetName = event.target.name;
    const targetValue = event.target.value;

    // We append newly entered value to the existing state
    setInfo2((info2) => ({
      ...info2,
      [targetName]: targetValue
    }));
  };

  const validate2 = () => {
    if (!info2.selectGrantDate) setError("Please pick a starting date.");
    else {
      setError("");
      props.nextStep();
      props.userCallback(info2);
    }
  };

  return (
    <div>
      <span style={{ color: "red" }}>{error}</span>
       <FormGroup>
        <Label for="selectGrantDropdown">
          What is your desired starting month?
        </Label>
        <Input
          id="selectGrantDateDropdown"
          name="selectGrantDate"
          type="select"
          onChange={onInputChanged}
        >
          <option hidden>
            Select starting month
          </option>
          <option>
            May 2024
          </option>
          <option>
            June 2024
          </option>
          <option>
            July 2024
          </option>
          <option>
            August 2024
          </option>
          <option>
            September 2024
          </option>
          <option>
            October 2024
          </option>
          <option>
            November 2024
          </option>
          <option>
            December 2024
          </option>
        </Input>
      </FormGroup>
      <br />
      <ActionButtons {...props} nextStep={validate2} />
    </div>
  );
};

const Three = (props) => {
  console.log("step3 receive user object");
  console.log(props.user);

  const handleLastStep = () => {
    props.lastStep();
    props.completeCallback();
  };

  return (
    <div>
      <br />
      <h3>Summary of application details</h3>
      <br />
      <p>Type: {props.user.selectGrant}</p>
      <p>Starting month: {props.user.selectGrantDate}</p>
      <p className="details-grant-summary"> Note that the amount of the grant will be recalculated every month. This depends on your living situation.</p>
      <ActionButtons {...props} lastStep={handleLastStep} />
    </div>
  );
};

const ProductApplication = () => {
  const [stepWizard, setStepWizard] = useState(null);
  const [user, setUser] = useState({});
  const [activeStep, setActiveStep] = useState(0);

  const assignStepWizard = (instance) => {
    setStepWizard(instance);
  };

  const assignUser = (val) => {
    console.log("parent received user callback - assigning user");
    console.log(val);
    setUser((user) => ({
      ...user,
      ...val
    }));
  };

  const handleStepChange = (e) => {
    console.log("step change");
    console.log(e);
    setActiveStep(e.activeStep - 1);
  };

  const handleComplete = () => {
    alert("Student grant application submitted succesfully! You will receive a confirmation e-mail within several minutes.");
  };

  return (
    <div className="grant-application-container">
      <Stepper activeStep={activeStep}>
        <Step label="Step 1" children={<MdDescription />} />
        <Step label="Step 2" />
        <Step label="Step 3" />
      </Stepper>
      {/* NOTE: IMPORTANT !! StepWizard must contains at least 2 children components, else got error */}
      <StepWizard instance={assignStepWizard} onStepChange={handleStepChange}>
        <One userCallback={assignUser} />
        <Two user={user} userCallback={assignUser} />
        <Three user={user} completeCallback={handleComplete} />
      </StepWizard>
    </div>
  );
};

export default ProductApplication;
