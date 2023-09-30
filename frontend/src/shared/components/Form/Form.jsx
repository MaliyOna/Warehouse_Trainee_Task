import React from "react";
import { useForm } from "react-hook-form";

export function Form({ defaultValues, children, onSubmit, mode }) {
  const methods = useForm({ defaultValues, mode: mode || "onSubmit" });
  const { handleSubmit } = methods;

  function recreateElement(child) {
    if (child?.props?.name) {
      return React.createElement(child.type, {
        ...child.props,
        register: methods.register,
        setValue: methods.setValue,
        key: child.props.name,
        error: methods.formState.errors[child.props.name]
      });
    }

    if (child?.props?.children) {

      const newChildren = React.Children.map(child.props.children, nestedChild => {
        return recreateElement(nestedChild);
      });

      return React.createElement(child.type, {
        ...child.props,
        children: newChildren
      })
    }

    return child;
  }


  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      {React.Children.map(children, child => {

        return recreateElement(child)
      })}
    </form>
  );
}